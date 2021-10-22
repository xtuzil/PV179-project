using System;

namespace Infrastructure.UnitOfWork
{
    public abstract class UnitOfWorkProviderBase : IUnitOfWorkProvider, IDisposable
    {
        protected IUnitOfWork unitOfWork { get; set; }

        public abstract IUnitOfWork Create();

        public IUnitOfWork GetUnitOfWorkInstance()
        {
            return unitOfWork;
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
