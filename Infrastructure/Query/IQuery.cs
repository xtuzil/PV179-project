using Infrastructure.Predicates;
using System.Threading.Tasks;

namespace Infrastructure.Query
{
    public interface IQuery<TEntity> where TEntity : class
    {
        public IQuery<TEntity> Where(IPredicate rootPredicate);
        public IQuery<TEntity> SortBy(string sortAccordingTo, bool ascendingOrder);
        public IQuery<TEntity> Page(int pageToFetch, int pageSize = 20);
        public Task<QueryResult<TEntity>> ExecuteAsync();
    }
}
