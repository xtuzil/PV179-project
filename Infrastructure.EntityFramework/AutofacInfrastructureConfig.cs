using Autofac;
using Module = Autofac.Module;
using System.Linq;
using System.Reflection;
using Infrastructure.Query;
using CactusDAL;
using Infrastructure.UnitOfWork;

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
