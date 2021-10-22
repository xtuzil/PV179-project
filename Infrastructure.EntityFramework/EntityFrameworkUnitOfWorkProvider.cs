using CactusDAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CactusDAL
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
