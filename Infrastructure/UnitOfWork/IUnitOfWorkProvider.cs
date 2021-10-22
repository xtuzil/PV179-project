namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWorkProvider
    {
        void Create();
        IUnitOfWork GetUnitOfWorkInstance();
    }
}
