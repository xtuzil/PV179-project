using CactusDAL.Models;
using System;

namespace CactusDAL
{
    public class UnitOfWork : IDisposable
    {
        private CactusDbContext context = new CactusDbContext();
        private GenericRepository<Cactus> cactusRepository;
        private GenericRepository<User> userRepository;

        public GenericRepository<Cactus> CactusRepository
        {
            get
            {

                if (this.cactusRepository == null)
                {
                    this.cactusRepository = new GenericRepository<Cactus>(context);
                }
                return cactusRepository;
            }
        }

        public GenericRepository<User> UserRepository
        {
            get
            {

                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(context);
                }
                return userRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}