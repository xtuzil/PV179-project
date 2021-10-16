using System.Threading.Tasks;
﻿using System.Collections.Generic;

namespace CactusDAL
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public Task<TEntity> GetAsync(int id);
        public Task<TEntity> GetAsync(int id, int[] includes);
        public IEnumerable<TEntity> GetAll();
        public void Create(TEntity entity);
        public void Update(TEntity entity);
        public void Delete(int id);
        public void Delete(TEntity entity);
    }
}
