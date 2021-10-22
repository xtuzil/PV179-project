using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.EntityFramework
{
    public class EntityFrameworkUnitOfWorkProvider : UnitOfWorkProviderBase
    {
        private Func<DbContext> dbContextFactory { get; set; }

        public EntityFrameworkUnitOfWorkProvider(Func<DbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public override IUnitOfWork Create()
        {
            unitOfWork = new EntityFrameworkUnitOfWork(dbContextFactory);
            return unitOfWork;
        }
    }
}
