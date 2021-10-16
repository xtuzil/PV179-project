using CactusDAL.Models;
using CactusDAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;

namespace CactusDAL
{
    public class EntityFrameworkUnitOfWork : UnitOfWorkBase
    {
        public DbContext _context { get; set; }

        private EntityFrameworkRepository<Cactus> cactusRepository;
        private EntityFrameworkRepository<User> userRepository;

        public EntityFrameworkUnitOfWork(DbContext dbContext)
        {
            _context = dbContext;
        }

        public EntityFrameworkRepository<Cactus> CactusRepository
        {
            get
            {

                if (this.cactusRepository == null)
                {
                    this.cactusRepository = new EntityFrameworkRepository<Cactus>(_context);
                }
                return cactusRepository;
            }
        }

        public EntityFrameworkRepository<User> UserRepository
        {
            get
            {

                if (this.userRepository == null)
                {
                    this.userRepository = new EntityFrameworkRepository<User>(_context);
                }
                return userRepository;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override void CommitCore()
        {
            _context.SaveChanges();
        }
    }
}