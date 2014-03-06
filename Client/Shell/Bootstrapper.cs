using Shell.Services;

namespace Shell
{
    public static class Bootstrapper
    {
        public static void InititializeServices()
        {
            // Services ermitteln und in den ServicePool aufnehmen
            new ServiceDiscoveryService().DiscoverServices(ServicePool.Current);
        }
    }
}
