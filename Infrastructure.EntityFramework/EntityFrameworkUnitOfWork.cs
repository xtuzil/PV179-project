using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.EntityFramework
{
    using TDbContext = DbContext;

    public class EntityFrameworkUnitOfWork : UnitOfWorkBase
    {
        public TDbContext Context { get; }

        public EntityFrameworkUnitOfWork(Func<TDbContext> dbContextFactory)
        {
            Context = dbContextFactory();
        }

        public EntityFrameworkUnitOfWork(TDbContext dbContext)
        {
            Context = dbContext;
        }

        protected override void CommitCore()
        {
            Context.SaveChanges();
        }

        public override void Dispose()
        {
            Context.Dispose();
        }
    }
}