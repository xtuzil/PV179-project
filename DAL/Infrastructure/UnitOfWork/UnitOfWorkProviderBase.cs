using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CactusDAL.UnitOfWork
{
    public abstract class UnitOfWorkProviderBase : IUnitOfWorkProvider
    {
        protected AsyncLocal<IUnitOfWork> _unitOfWorkLocalInstance { get; set; }

        public abstract void Create();

        public IUnitOfWork GetUnitOfWorkInstance()
        {
            return _unitOfWorkLocalInstance.Value;
        }

        public void Dispose() {
            _unitOfWorkLocalInstance.Value.Dispose();
        }
    }
}
