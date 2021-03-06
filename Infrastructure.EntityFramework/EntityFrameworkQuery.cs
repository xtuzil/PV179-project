using Infrastructure.Predicates;
using Infrastructure.Predicates.Operators;
using Infrastructure.Query;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.EntityFramework
{
    public class EntityFrameworkQuery<TEntity> : QueryBase<TEntity> where TEntity : class
    {
        protected DbContext context;
        internal IQueryable<TEntity> _query;
        private string LambdaParameterName { get; set; }
        private ParameterExpression parameterExpression { get; set; }

        public EntityFrameworkQuery(IUnitOfWorkProvider provider) : base(provider)
        {
            context = ((EntityFrameworkUnitOfWork)provider.Create()).Context;
            LambdaParameterName = typeof(TEntity).GetType().Name;
            parameterExpression = Expression.Parameter(typeof(TEntity), LambdaParameterName);
            _query = context.Set<TEntity>();
        }

        public override async Task<QueryResult<TEntity>> ExecuteAsync()
        {
            IQueryable<TEntity> result = _query;
            IList<TEntity> resultList;
            var queryResult = new QueryResult<TEntity>();
            queryResult.TotalItemsCount = _query.Count();
            if (Predicate != null)
            {
                result = UseFilterCriteria(result);
                queryResult.TotalItemsCount = result.Count();
            }

            if (DesiredPage != 0)
            {
                result = result.Skip((DesiredPage - 1) * PageSize).Take(PageSize);
            }

            if (SortAccordingTo != null)
            {
                result = UseSortCriteria<object>(result);
            }
            resultList = await result.ToListAsync();

            queryResult.RequestedPageNumber = DesiredPage;
            queryResult.PageSize = PageSize;
            queryResult.PagingEnabled = DesiredPage == 0 ? false : true;
            queryResult.Items = new List<TEntity>(resultList);

            return queryResult;
        }

        private IQueryable<TEntity> UseSortCriteria<TKey>(IQueryable<TEntity> queryable)
        {
            Expression<Func<TEntity, TKey>> sortExpression =
                Expression.Lambda<Func<TEntity, TKey>>(
                    Expression.Property(parameterExpression, LambdaParameterName),
                    parameterExpression
                );

            return UseSortCriteriaCore(sortExpression, queryable);
        }

        private IQueryable<TEntity> UseSortCriteriaCore<TKey>(Expression<Func<TEntity, TKey>> sortExpression, IQueryable<TEntity> queryable)
        {
            if (UseAscendingOrder)
            {
                return queryable.OrderBy(sortExpression);
            }

            return queryable.OrderByDescending(sortExpression);
        }

        private IQueryable<TEntity> UseFilterCriteria(IQueryable<TEntity> queryable)
        {
            return queryable.Where(
                Expression.Lambda<Func<TEntity, bool>>(
                    BuildBinaryExpression(Predicate),
                    parameterExpression
                )
            );
        }

        private Expression CombineBinaryExpressions(CompositePredicate compositePredicate)
        {
            List<IPredicate> predicates = compositePredicate.Predicates;

            Expression leftExpression = BuildBinaryExpression(predicates[0]);
            Expression rightExpression = BuildBinaryExpression(predicates[1]);

            BinaryExpression result = compositePredicate.LogicalOperator == LogicalOperator.AND
                ? Expression.And(leftExpression, rightExpression)
                : Expression.Or(leftExpression, rightExpression);

            if (predicates.Count > 2)
            {
                for (int i = 2; i < predicates.Count; i++)
                {
                    Expression inner = BuildBinaryExpression(predicates[i]);
                    result = compositePredicate.LogicalOperator == LogicalOperator.AND
                        ? Expression.AndAlso(result, inner)
                        : Expression.OrElse(result, inner);
                }
            }

            return result;
        }

        private Expression BuildBinaryExpression(IPredicate predicate)
        {
            if (predicate.GetType() == typeof(SimplePredicate))
            {
                SimplePredicate simplePredicate = (SimplePredicate)predicate;
                return simplePredicate.GetExpression(parameterExpression);
            }

            return CombineBinaryExpressions((CompositePredicate)predicate);
        }
    }
}
