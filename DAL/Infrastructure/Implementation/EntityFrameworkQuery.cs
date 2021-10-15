using CactusDAL.Predicates;
using CactusDAL.UnitOfWork;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CactusDAL.Query
{
    public class EntityFrameworkQuery<TEntity, TKey> : QueryBase<TEntity> where TEntity : class
    {
        protected DbContext context;
        private string LambdaParameterName;
        private ParameterExpression parameterExpression;

        public EntityFrameworkQuery(EntityFrameworkUnitOfWorkProvider provider) : base(provider)
        {
            context = provider.Create();
        }

        public override Task<QueryResult<TEntity>> ExecuteAsync()
        {
            throw new NotImplementedException();
        }

        public override IQuery<TEntity> Where(IPredicate rootPredicate)
        {
            base.Where(rootPredicate);

            UseFilterCriteria();
        }

        private void UseSortCriteria(IQueryable<TEntity> queryable)
        {

        } 

        private void UseSortCriteriaCore(Expression<Func<TEntity, TKey>> sortExpression, IQueryable<TEntity> queryable)
        {

        }

        private void UseFilterCriteria(IQueryable<TEntity> queryable) {
            
        }

        private string CombineBinaryExpressions(CompositePredicate compositePredicate) {
            List<IPredicate> predicates = compositePredicate.Predicates;
            string resultingPredicate = "";

            for (int i = 0; i < predicates.Count; i++)
            {
                resultingPredicate += BuildBinaryExpression(predicates[i]);

                if (i < predicates.Count - 1)
                {
                    resultingPredicate += compositePredicate.LogicalOperator;
                }
            }

            return resultingPredicate;
        }

        private string BuildBinaryExpression(IPredicate predicate)
        {
            if (predicate.GetType() == typeof(SimplePredicate))
            {
                SimplePredicate simplePredicate = (SimplePredicate)predicate;

                return $"{simplePredicate.TargetPropertyName} {simplePredicate.ValueComparingOperator} {simplePredicate.ComparedValue}";
            }

            return CombineBinaryExpressions((CompositePredicate)predicate);
            
        }
    }
}
