using CactusDAL.Predicates;
using CactusDAL.UnitOfWork;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CactusDAL.Query
{
    public abstract class QueryBase<TEntity> : IQuery<TEntity> where TEntity : class
    {
        public int PageSize { get; set; }
        public int DesiredPage { get; set; }
        public string SortAccordingTo { get; set; }
        public bool UseAscendingOrder { get; set; }
        public IPredicate Predicate { get; set; }
        protected IUnitOfWorkProvider _provider { get; set; }
        private const int _defaultPageSize = 20;

        public QueryBase(IUnitOfWorkProvider provider)
        {
            _provider = provider;
        }

        public virtual IQuery<TEntity> Where(IPredicate rootPredicate)
        {
            Predicate = rootPredicate;
            PageSize = _defaultPageSize;

            return this;
        }

        public virtual IQuery<TEntity> SortBy<TKey>(string sortAccordingTo, bool ascendingOrder)
        {
            SortAccordingTo = sortAccordingTo;
            UseAscendingOrder = ascendingOrder;

            return this;
        }

        public virtual IQuery<TEntity> Page(int pageToFetch, int pageSize = _defaultPageSize)
        {
            PageSize = pageSize;
            DesiredPage = pageToFetch;

            return this;
        }

        public abstract Task<QueryResult<TEntity>> ExecuteAsync();
    }
}
