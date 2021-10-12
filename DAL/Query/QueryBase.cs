using CactusDAL.Predicates;
using System;
using System.Threading.Tasks;

namespace CactusDAL.Query
{
    public class QueryBase<TEntity> : IQuery<TEntity> where TEntity : class
    {
        public int PageSize { get; set; }
        public int DesiredPage { get; set; }
        public string SortAccordingTo { get; set; }
        public bool UseAscendingOrder { get; set; }
        public IPredicate Predicate { get; set; }

        private int _defaultPageSize = 20;

        public IQuery<TEntity> Where(IPredicate rootPredicate)
        {
            throw new NotImplementedException();
        }

        public IQuery<TEntity> SortBy(string sortAccordingTo, bool ascendingOrder)
        {
            throw new NotImplementedException();
        }

        public IQuery<TEntity> Page(int pageToFetch, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<QueryResult<TEntity>> ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
