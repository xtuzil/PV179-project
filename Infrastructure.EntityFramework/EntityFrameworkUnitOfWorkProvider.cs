using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.EntityFramework
{
    public class EntityFrameworkUnitOfWorkProvider : UnitOfWorkProviderBase
    {
        private Func<DbContext> _dbContextFactory { get; set; }

        public EntityFrameworkUnitOfWorkProvider(Func<DbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public override void Create()
        {
            _unitOfWorkLocalInstance.Value = new EntityFrameworkUnitOfWork(_dbContextFactory());
        }
    }
}
