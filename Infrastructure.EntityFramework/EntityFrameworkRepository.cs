using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.EntityFramework
{
    using TDbContext = DbContext;

    public class EntityFrameworkRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity<int>
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

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> GetAsync(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> result = Context.Set<TEntity>();
            foreach (var include in includes)
            {
                result = result.Include(include);
            }

            return await result.Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> Create(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
            return entity.Id;
        }

        public async Task Delete(int id)
        {
            TEntity entityToDelete = await Context.Set<TEntity>().FindAsync(id);
            Delete(entityToDelete);
        }

        public void Delete(TEntity entityToDelete)
        {
            Context.Set<TEntity>().Remove(entityToDelete);
        }

        public void Update(TEntity entityToUpdate)
        {
            Context.Set<TEntity>().Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}