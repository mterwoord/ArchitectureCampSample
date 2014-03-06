using System;

namespace Contracts
{
    public interface IServicePool
    {
        void AddService<T>(T service);
        void AddService(Type serviceType, object service);
        T GetService<T>();
        void RemoveService<T>();
        void RemoveService(Type serviceType);
    }
}
