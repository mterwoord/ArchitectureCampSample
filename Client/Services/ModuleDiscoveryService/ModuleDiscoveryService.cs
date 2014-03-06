using Contracts;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Services
{
    public class ModuleDiscoveryService : IModuleDiscoveryService
    {
        private CompositionContainer _container;

        public ModuleDiscoveryService()
        {
            this.Compose();
        }

        public IServicePool ServicePool { get; set; }

        private void Compose()
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var catalog = new DirectoryCatalog(path);
            _container = new CompositionContainer(catalog);
            _container.SatisfyImportsOnce(this);
        }

        public List<IModuleMetadata> GetModulesMetadata()
        {
            var list = new List<IModuleMetadata>();
            var modules = _container.GetExports<IModule, IModuleMetadata>();
            modules.ToList().ForEach(module => list.Add(module.Metadata));
            return (from m in list orderby m.DisplayIndex select m).ToList();
        }

        public IModule ActivateModule(IModuleMetadata metadata)
        {
            var modules = _container.GetExports<IModule, IModuleMetadata>();
            var module = (from m in modules
                          where m.Metadata.Name.Equals(metadata.Name)
                          select m.Value).FirstOrDefault();
            if (module != null)
            {
                module.Initialize(this.ServicePool);
            }
            return module;
        }
    }
}
