using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IRepository<TEntity> where TEntity : IEntity<int>
    {
        public Task<TEntity> GetAsync(int id);
        public Task<TEntity> GetAsync(int id, params Expression<Func<TEntity, object>>[] includes);
        public Task<IEnumerable<TEntity>> GetAll();
        public Task<int> Create(TEntity entity);
        public void Update(TEntity entity);
        public Task Delete(int id);
        public void Delete(TEntity entity);
    }
}
