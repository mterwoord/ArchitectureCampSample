using Contracts;
using System;
using System.Collections.Generic;

namespace Shell.Services
{
    public class ServicePool : IServicePool
    {
        private static ServicePool _current;
        private static object _lockObject = new object();
        private Dictionary<Type, object> _services;

        private ServicePool()
        {
            _services = new Dictionary<Type, object>();
        }

        public static ServicePool Current
        {
            get
            {
                lock (_lockObject)
                {
                    if (_current == null)
                        _current = new ServicePool();
                    return _current;
                }
            }
        }

        public void AddService<T>(T service)
        {
            this.AddService(typeof(T), service);
        }

        public void AddService(Type serviceType, object service)
        {
            if (!_services.ContainsKey(serviceType))
                //throw new InvalidOperationException(string.Format("A service of type \"{0}\" already exists.", serviceType.Name));
                _services.Add(serviceType, service);
        }

        public T GetService<T>()
        {
            var requestedType = typeof(T);
            foreach (var item in _services)
            {
                if (requestedType.IsAssignableFrom(item.Key))
                    return (T)item.Value;
            }
            return default(T);
        }

        public void RemoveService<T>()
        {
            this.RemoveService(typeof(T));
        }

        public void RemoveService(Type serviceType)
        {
            if (_services.ContainsKey(serviceType))
                _services.Remove(serviceType);
        }
    }
}
