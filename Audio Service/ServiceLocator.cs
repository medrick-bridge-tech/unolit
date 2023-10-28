using System.Collections.Generic;
using Medrick.Unolit.Utility;

namespace Medrick.Unolit.Service
{
    public class ServiceLocator : Singleton<ServiceLocator>
    {
        private Dictionary<string, Service> services = new Dictionary<string, Service>();

        public T Locate<T>() where T : Service
        {
            var name = typeof(T).Name;
            if (services.ContainsKey(name))
                return (T)services[name];
            return default;
        }
        
        public void Register<T>(T service) where T : Service
        {
            var name = typeof(T).Name;
            if (services.ContainsKey(name) == false)
                services.Add(name, service);
        }

        public void Unregister<T>() where T : Service
        {
            var name = typeof(T).Name;
            if (services.ContainsKey(name))
                services.Remove(name);
        } 
    }
}