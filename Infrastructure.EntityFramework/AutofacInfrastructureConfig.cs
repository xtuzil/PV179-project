using Autofac;
using Module = Autofac.Module;
using System.Linq;
using System.Reflection;
using Infrastructure.Query;

namespace Infrastructure.EntityFramework
{
    public class AutofacInfrastructureConfig : Module
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterGeneric(typeof(EntityFrameworkQuery<>))
                .As(typeof(IQuery<>))
                .InstancePerDependency();

            return builder.Build();
        }
    }
}
