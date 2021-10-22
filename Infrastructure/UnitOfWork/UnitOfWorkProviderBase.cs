using System;
using System.Threading;

namespace Infrastructure.UnitOfWork
{
    public abstract class UnitOfWorkProviderBase : IUnitOfWorkProvider, IDisposable
    {
        protected AsyncLocal<IUnitOfWork> _unitOfWorkLocalInstance { get; set; } = new AsyncLocal<IUnitOfWork>();

        public abstract void Create();

        public IUnitOfWork GetUnitOfWorkInstance()
        {
            return _unitOfWorkLocalInstance.Value;
        }

        public void Dispose()
        {
            _unitOfWorkLocalInstance.Value = null;
        }
    }
}
