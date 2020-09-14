using RestApi.Client.Clients;
using RestApi.Tests.ResourceCreators.Interfaces;
using RestApi.Tests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity;

namespace RestApi.Tests.Unity
{
    public class TestsUnityContainer : ITestsUnityContainer
    {
        private readonly UnityContainer _unityContainer;

        public TestsUnityContainer()
        {
            _unityContainer = new UnityContainer();

            foreach (var assembly in GetAssemblies())
            {
                AddAssembly(assembly);
                AddDeleters(assembly);
            }
        }

        protected IEnumerable<Assembly> GetAssemblies()
        {
            var config = new ConfigHandler().Config;
            foreach (var assemblyName in config.Assemblies)
            {
                yield return Assembly.LoadFrom(assemblyName + ".dll");
            } 
        }

        public T Resolve<T>()
        {
            return _unityContainer.Resolve<T>();
        }

        public T Resolve<T>(string name)
        {
            return _unityContainer.Resolve<T>(name);
        }

        private void AddAssembly(Assembly assembly)
        {
            var classTypes = assembly.GetTypes();
            foreach (var classType in classTypes)
                foreach (var interfaceType in classType.GetInterfaces())
                    _unityContainer.RegisterSingleton(interfaceType, classType);
        }
        private void AddDeleters(Assembly assembly)
        {
            var resourceGettersClassTypes = assembly.GetTypes().Where(t =>t.IsClass && !t.IsAbstract && t.GetInterfaces().Any(IsResourceGetter));
            foreach (var resourceGetterClassTypes in resourceGettersClassTypes)
            {
                var genericArguments = resourceGetterClassTypes.BaseType?.GetGenericArguments();
                if (genericArguments?.Length > 0)
                {
                    var resourceName = genericArguments[1].Name;
                    _unityContainer.RegisterSingleton(typeof(IResourceDeleter), resourceGetterClassTypes, resourceName);
                }
            }
        }

        private static bool IsResourceGetter(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IResourceGetter<>);
        }
    }
}
