using CactusDAL.Predicates;
using System.Threading.Tasks;

namespace CactusDAL.Query
{
    public interface IQuery<TEntity> where TEntity : class
    {
        public IQuery<TEntity> Where(IPredicate rootPredicate);
        public IQuery<TEntity> SortBy<TKey>(string sortAccordingTo, bool ascendingOrder);
        public IQuery<TEntity> Page(int pageToFetch, int pageSize = 20);
        public Task<QueryResult<TEntity>> ExecuteAsync();
    }
}
