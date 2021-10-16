using CactusDAL.Infrastructure;
using CactusDAL.Predicates;
using CactusDAL.UnitOfWork;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CactusDAL.Query
{
    public class EntityFrameworkQuery<TEntity> : QueryBase<TEntity> where TEntity : class
    {
        protected DbContext context;
        private string LambdaParameterName { get; set; }
        private ParameterExpression parameterExpression { get; set; }
        private IQueryable<TEntity> _queryable { get; set; }

        public EntityFrameworkQuery(EntityFrameworkUnitOfWorkProvider provider, IQueryable<TEntity> queryable) : base(provider)
        {
            context = ((EntityFrameworkUnitOfWork) provider.GetUnitOfWorkInstance()).Context;
            LambdaParameterName = typeof(TEntity).GetType().Name;
            parameterExpression = Expression.Parameter(typeof(TEntity), LambdaParameterName);
            _queryable = queryable;
        }

        public override async Task<QueryResult<TEntity>> ExecuteAsync()
        {
            QueryResult<TEntity> queryResult = new QueryResult<TEntity>();
            queryResult.TotalItemsCount = _queryable.Count();
            queryResult.RequestedPageNumber = DesiredPage;
            queryResult.PageSize = PageSize;
            queryResult.PagingEnabled = DesiredPage == 0 ? false : true;
            queryResult.Items = new List<TEntity>(_queryable);

            return queryResult;
        }

        public override EntityFrameworkQuery<TEntity> Where(IPredicate rootPredicate)
        {
            base.Where(rootPredicate);

            IUnitOfWork unitOfWork = _provider.GetUnitOfWorkInstance();
            unitOfWork.RegisterAction(() => UseFilterCriteria(_queryable));

            return this;
        }

        public override EntityFrameworkQuery<TEntity> SortBy<TKey>(string sortAccordingTo, bool ascendingOrder)
        {
            base.SortBy<TKey>(sortAccordingTo, ascendingOrder);

            IUnitOfWork unitOfWork = _provider.GetUnitOfWorkInstance();
            unitOfWork.RegisterAction(() => UseSortCriteria<TKey>(_queryable));

            return this;
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

        private IQueryable<TEntity> UseFilterCriteria(IQueryable<TEntity> queryable) {
            return queryable.Where(
                Expression.Lambda<Func<TEntity, bool>>(
                    BuildBinaryExpression(Predicate),
                    parameterExpression
                )
            );
        }

        private Expression CombineBinaryExpressions(CompositePredicate compositePredicate) {
            List<IPredicate> predicates = compositePredicate.Predicates;

            Expression leftExpression = BuildBinaryExpression(predicates[0]);
            Expression rightExpression = BuildBinaryExpression(predicates[1]);

            BinaryExpression result = compositePredicate.LogicalOperator == Predicates.Operators.LogicalOperator.AND
                ? Expression.And(leftExpression, rightExpression)
                : Expression.Or(leftExpression, rightExpression);

            if(predicates.Count > 2)
            {
                for(int i = 2; i < predicates.Count; i++)
                {
                    Expression inner = BuildBinaryExpression(predicates[i]);
                    result = compositePredicate.LogicalOperator == Predicates.Operators.LogicalOperator.AND
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
