using CactusDAL.Query;
using CactusDAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CactusDAL
{
    public class EntityFrameworkRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext _context;
        internal DbSet<TEntity> _dbSet;
        private IUnitOfWorkProvider _provider;

        public EntityFrameworkRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(IQuery<TEntity> query)
        {
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ExecuteAsync();
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<TEntity> GetAsync(int id, int[] includes)
        {
            return await _dbSet.FindAsync(id); 
        }

        public virtual void Create(TEntity entity)
        {
            IUnitOfWork unitOfWork = _provider.GetUnitOfWorkInstance();
            unitOfWork.RegisterAction(() => _dbSet.Add(entity));
        }

        public virtual void Delete(int id)
        {
            IUnitOfWork unitOfWork = _provider.GetUnitOfWorkInstance();
            TEntity entityToDelete = _dbSet.Find(id);
            unitOfWork.RegisterAction(() => Delete(entityToDelete));
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            IUnitOfWork unitOfWork = _provider.GetUnitOfWorkInstance();
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                unitOfWork.RegisterAction(() => _dbSet.Attach(entityToDelete));
            }
            unitOfWork.RegisterAction(() => _dbSet.Remove(entityToDelete));
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            IUnitOfWork unitOfWork = _provider.GetUnitOfWorkInstance();
            unitOfWork.RegisterAction(() =>
            {
                _dbSet.Attach(entityToUpdate);
                _context.Entry(entityToUpdate).State = EntityState.Modified;
            });
        }
    }
}