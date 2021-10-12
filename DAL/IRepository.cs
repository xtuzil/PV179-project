namespace CactusDAL
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public TEntity GetById(int id);
        public void Create(TEntity entity);
        public void Update(TEntity entity);
        public void Delete(int id);
        public void Delete(TEntity entity);
    }
}
