using System;

namespace Contracts
{
    public interface IServiceDiscoveryService
    {
        void DiscoverServices(IServicePool pool);
    }
}
