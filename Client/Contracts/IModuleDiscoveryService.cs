using System.Collections.Generic;

namespace Contracts
{
    public interface IModuleDiscoveryService : IService
    {
        List<IModuleMetadata> GetModulesMetadata();
        IModule ActivateModule(IModuleMetadata metadata);
    }
}
