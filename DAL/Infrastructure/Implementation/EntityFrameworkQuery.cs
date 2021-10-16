using CactusDAL.Infrastructure;
using CactusDAL.Predicates;
using CactusDAL.UnitOfWork;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CactusDAL.Query
{
    public class EntityFrameworkQuery<TEntity, TKey> : QueryBase<TEntity> where TEntity : class
    {
        protected DbContext context;
        private string LambdaParameterName { get; set; }
        private ParameterExpression parameterExpression { get; set; }

        public EntityFrameworkQuery(EntityFrameworkUnitOfWorkProvider provider) : base(provider)
        {
            context = ((EntityFrameworkUnitOfWork) provider.GetUnitOfWorkInstance())._context;
            LambdaParameterName = typeof(TEntity).GetType().Name;
            parameterExpression = Expression.Parameter(typeof(TEntity), LambdaParameterName);
        }

        public override Task<QueryResult<TEntity>> ExecuteAsync()
        {
            throw new NotImplementedException();
        }

        public override IQuery<TEntity> Where(IPredicate rootPredicate)
        {
            base.Where(rootPredicate);

            IUnitOfWork unitOfWork = _provider.GetUnitOfWorkInstance();
            unitOfWork.RegisterAction(() => UseFilterCriteria(this));

            return this;
        }

        public override IQuery<TEntity> SortBy(string sortAccordingTo, bool ascendingOrder)
        {
            base.SortBy(sortAccordingTo, ascendingOrder);

            IUnitOfWork unitOfWork = _provider.GetUnitOfWorkInstance();
            unitOfWork.RegisterAction(() => UseSortCriteria(this));
        }

        public override IQuery<TEntity> Page(int pageToFetch, int pageSize)
        {
            base.Page(pageToFetch, pageSize);

            IUnitOfWork unitOfWork = _provider.GetUnitOfWorkInstance();
            unitOfWork.RegisterAction(() => query);
        }

        private IQueryable<TEntity> UseSortCriteria(IQueryable<TEntity> queryable)
        {
            Expression<Func<TEntity, TKey>> sortExpression =
                Expression.Lambda<Func<TEntity, TKey>>(
                    Expression.Property(parameterExpression, LambdaParameterName),
                    parameterExpression
                );


            return UseSortCriteriaCore(sortExpression, queryable);
        } 

        private IQueryable<TEntity> UseSortCriteriaCore(Expression<Func<TEntity, TKey>> sortExpression, IQueryable<TEntity> queryable)
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
