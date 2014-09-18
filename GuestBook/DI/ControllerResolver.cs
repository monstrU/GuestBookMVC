using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StructureMap;

namespace GuestBook.DI
{
    public class ControllerResolver : IDependencyResolver
    {
        public object GetService(Type serviceType)
        {
            
            var instance = ObjectFactory.Container.TryGetInstance(serviceType);

            if (instance == null && !serviceType.IsAbstract && !serviceType.IsInterface)
            {
                instance = ObjectFactory.GetInstance(serviceType);
            }

            return instance;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return ObjectFactory.GetAllInstances(serviceType).Cast<object>();
        }
    }
}