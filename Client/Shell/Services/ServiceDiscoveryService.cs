using Contracts;
using System;
//using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.IO;
using System.Reflection;

namespace Shell.Services
{
    public class ServiceDiscoveryService : IServiceDiscoveryService
    {
        public void DiscoverServices(IServicePool pool)
        {
            var registration = new RegistrationBuilder();

            registration.ForTypesDerivedFrom<IService>().SelectConstructor((ConstructorInfo[] cInfo) =>
            {
                if (cInfo.Length == 0)
                    return null;
                return cInfo[0];
            }).Export<IService>();

            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var catalog = new DirectoryCatalog(path, registration);
            var container = new CompositionContainer(catalog, CompositionOptions.DisableSilentRejection);
            var services = container.GetExportedValues<IService>();

            foreach (var service in services)
            {
                service.ServicePool = pool;
                var interfaces = service.GetType().GetInterfaces();
                Type interfaceType = null;
                foreach (var i in interfaces)
                {
                    var name = i.FullName;
                    if (!name.Contains("Contracts.IService") && !name.Contains("System."))
                    {
                        interfaceType = i;
                        break;
                    }
                }
                pool.AddService(interfaceType, service);
            }
        }
    }
}
