using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;

namespace Hosting
{
    internal class ContainerConfiguration
    {
        public static AutofacWebApiDependencyResolver GetWebApiDependencyResolver()
        {
            var builder = new ContainerBuilder();

            var webApiAssembly = Assembly.Load("ServicesLayer");
            builder.RegisterApiControllers(webApiAssembly);

            var businessLogicAssembly = Assembly.Load("BusinessLogicLayer");
            builder.RegisterAssemblyTypes(businessLogicAssembly)
                .AsImplementedInterfaces()
                .InstancePerApiRequest();

            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);

            return resolver;
        }
    }
}