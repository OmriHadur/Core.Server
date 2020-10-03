using Core.Server.Common.Entities;
using Core.Server.Shared.Resources;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Core.Server.Web.Utils
{
    public class ReflactionHelper : IReflactionHelper
    {
        private List<Type> types;

        public ReflactionHelper(IConfiguration configuration)
        {
            types = new List<Type>();
            var assembliesName = configuration.GetSection(Config.AssembliesSection).Get<string[]>();

            foreach (Assembly assembly in GetAssemblies(assembliesName))
                types.AddRange(assembly.GetExportedTypes());
        }

        protected virtual IEnumerable<Assembly> GetAssemblies(string[] assembliesName)
        {
            foreach (var assemblyName in assembliesName)
            {
                var path = $"{AppDomain.CurrentDomain.BaseDirectory}\\{assemblyName}.dll";
                yield return Assembly.LoadFrom(path);
            }
        }

        public IEnumerable<ResourceBoundle> GetResourcesBoundles()
        {
            var resourceTypes = GetDrivenTypesOf<Resource>();

            foreach (var resourceType in resourceTypes)
                yield return GetResourceTypes(resourceType);
        }

        private ResourceBoundle GetResourceTypes(Type resourceType)
        {
            var resourceName = GetTypeName(resourceType, typeof(Resource));
            return new ResourceBoundle()
            {
                ResourceType = resourceType,
                CreateResourceType = GetTypeWithName<CreateResource>(resourceName),
                UpdateResourceType = GetTypeWithName<UpdateResource>(resourceName),
                EntityType = GetTypeWithName<Entity>(resourceName)
            };
        }

        public Type MakeGenericType(Type genericType, ResourceBoundle resourceBoundle)
        {
            var genericArguments = GetGenericArguments(genericType, resourceBoundle).ToArray();
            return genericType.MakeGenericType(genericArguments);
        }
        private IEnumerable<Type> GetGenericArguments(Type genericType, ResourceBoundle resourceBoundle)
        {
            return genericType.GetGenericArguments()
                .Select(ga => resourceBoundle.GetSameBaseType(ga));
        }
        public string GetTypeName(Type drivenType, Type subType)
        {
            return drivenType.Name.Replace(subType.Name, string.Empty);
        }

        public IEnumerable<Type> GetDrivenTypesOf<T>()
        {
            return GetDrivenTypesOf(typeof(T));
        }

        public IEnumerable<Type> GetDrivenTypesOf(Type type)
        {
            return types.Where(t => t.BaseType == type);
        }

        public IEnumerable<Type> GetTypesWithAttribute<TAttribute>()
            where TAttribute : Attribute
        {
            return types.Where(t => t.GetCustomAttribute<TAttribute>() != null);
        }

        public Type GetTypeWithName<T>(string name)
        {
            var updateResourceName = name + typeof(T).Name;
            return types.FirstOrDefault(t => t.Name == updateResourceName);
        }
    }
}
