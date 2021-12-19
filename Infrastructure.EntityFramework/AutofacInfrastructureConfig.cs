using Autofac;
using CactusDAL;
using Infrastructure.Query;
using Infrastructure.UnitOfWork;
using Module = Autofac.Module;

namespace Infrastructure.EntityFramework
{
    public class AutofacInfrastructureConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(new EntityFrameworkUnitOfWorkProvider(() => new CactusDbContext()))
               .As<IUnitOfWorkProvider>()
               .SingleInstance().PreserveExistingDefaults();

            builder.RegisterGeneric(typeof(EntityFrameworkQuery<>))
                .As(typeof(IQuery<>))
                .InstancePerDependency();

            builder.RegisterGeneric(typeof(EntityFrameworkRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerDependency();
        }

        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(new EntityFrameworkUnitOfWorkProvider(() => new CactusDbContext()))
               .As<IUnitOfWorkProvider>()
               .SingleInstance().PreserveExistingDefaults();

            builder.RegisterGeneric(typeof(EntityFrameworkQuery<>))
                .As(typeof(IQuery<>))
                .InstancePerDependency();

            builder.RegisterGeneric(typeof(EntityFrameworkRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerDependency();

            return builder.Build();
        }
    }
}
