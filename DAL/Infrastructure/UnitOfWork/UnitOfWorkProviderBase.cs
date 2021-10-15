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

        public abstract IUnitOfWork Create();
        public IUnitOfWork GetUnitOfWorkInstance()
        {
            _unitOfWorkLocalInstance.Value = Create();
        }
        public void Dispose() { }
    }
}
