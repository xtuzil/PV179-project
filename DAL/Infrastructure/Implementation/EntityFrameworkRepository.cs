using CactusDAL.Predicates;
using CactusDAL.Query;
using CactusDAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CactusDAL
{
    public class EntityFrameworkRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext _context;
        internal DbSet<TEntity> _dbSet;
        private EntityFrameworkUnitOfWorkProvider _provider;

        public EntityFrameworkRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual Task<QueryResult<TEntity>> Get(
            IPredicate? predicate,
            string? sortAccordingTo,
            bool? useAscendingOrder,
            int? pageSize,
            int? desiredPage)
        {
            IQuery<TEntity> query = new EntityFrameworkQuery<TEntity>(_provider, _dbSet);

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            
            if (sortAccordingTo != null && useAscendingOrder != null)
            {
                query = query.SortBy<object>(sortAccordingTo, (bool)useAscendingOrder);
            }

            if (desiredPage != null)
            {
                if (pageSize != null)
                {
                    query = query.Page((int)desiredPage, (int)pageSize);
                }

                query = query.Page((int)desiredPage);
            }
            return query.ExecuteAsync();
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
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