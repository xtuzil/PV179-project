using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.EntityFramework
{
    using TDbContext = DbContext;

    public class EntityFrameworkRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly EntityFrameworkUnitOfWorkProvider unitOfWorkProvider;

        protected virtual TDbContext Context
        {
            get
            {
                return ((EntityFrameworkUnitOfWork)unitOfWorkProvider.GetUnitOfWorkInstance()).Context;
            }
        }

        public EntityFrameworkRepository(IUnitOfWorkProvider unitOfWorkProvider)
        {
            this.unitOfWorkProvider = (EntityFrameworkUnitOfWorkProvider) unitOfWorkProvider;
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public virtual async Task<TEntity> GetAsync(int id, int[] includes)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public virtual void Create(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public virtual void Delete(int id)
        {
            Delete(Context.Set<TEntity>().Find(id));
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            Context.Set<TEntity>().Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            Context.Set<TEntity>().Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}