using Autofac;
using Module = Autofac.Module;
using System.Linq;
using System.Reflection;
using Infrastructure;
using AutoMapper;
using BL.Config;
using Infrastructure.UnitOfWork;
using CactusDAL;
using Infrastructure.EntityFramework;

namespace BL
{
    public class AutofacBLConfig : Module
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            // TODO create AutofacInfrastructureConfig() - class already exists in Infrastructure.EntityFramework
            builder.RegisterModule(new AutofacInfrastructureConfig());

            // TODO: register QueryObject
            builder.RegisterGeneric(typeof(QueryObject<,>))
              .As(typeof(QueryObject<,>))
              .InstancePerLifetimeScope();


            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace == "BL.Services")
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name))
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace == "BL.Facades")
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name))
                .InstancePerDependency();

            builder.RegisterInstance(new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping)))
                 .As<IMapper>()
                 .SingleInstance();

            builder.RegisterInstance(new EntityFrameworkUnitOfWorkProvider(() => new CactusDbContext()))
               .As<IUnitOfWorkProvider>()
               .SingleInstance().PreserveExistingDefaults();

            return builder.Build();
        }

    }
}
