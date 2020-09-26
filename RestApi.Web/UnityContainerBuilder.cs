using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using RestApi.Application;
using RestApi.Common;
using RestApi.Persistence.Repositories;
using RestApi.Shared.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Unity;

namespace RestApi.Web
{
    public class UnityContainerBuilder
    {
        public void ConfigureContainer(IUnityContainer container, IConfiguration configuration)
        {
            var assembliesName = configuration.GetSection(Config.AssembliesSection).Get<string[]>();

            foreach (Assembly assembly in GetAssemblies(assembliesName))
                AddAssembly(assembly, container);
        }

        protected virtual IEnumerable<Assembly> GetAssemblies(string[] assembliesName)
        {
            foreach (var assemblyName in assembliesName)
            {
                var path = $"{AppDomain.CurrentDomain.BaseDirectory}\\{assemblyName}.dll";
                yield return Assembly.LoadFrom(path);
            }
        }

        private void AddAssembly(Assembly assembly, IUnityContainer container)
        {
            AddInjectClasses(assembly, container);
            AddInjectManyClasses(assembly, container);
        }

        private static void AddInjectClasses(Assembly assembly, IUnityContainer container)
        {
            var classTypes = GetClassesWithAttribute<Common.InjectAttribute>(assembly);
            foreach (var classType in classTypes)
                foreach (var interfaceType in classType.GetInterfaces())
                    container.RegisterType(interfaceType, classType);
        }

        private static void AddInjectManyClasses(Assembly assembly, IUnityContainer container)
        {
            var classTypes = GetClassesWithAttribute<InjectManyAttribute>(assembly);
            foreach (var classType in classTypes)
                foreach (var interfaceType in classType.GetInterfaces())
                    container.RegisterType(interfaceType, classType, classType.Name);
        }

        private static IEnumerable<Type> GetClassesWithAttribute<TAttribute>(Assembly assembly)
            where TAttribute: Attribute
        {
            return assembly.GetTypes()
                .Where(t => t.GetCustomAttribute<TAttribute>() != null);
        }
    }
}
