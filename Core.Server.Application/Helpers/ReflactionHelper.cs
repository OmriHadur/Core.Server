using Core.Server.Common;
using Core.Server.Common.Config;
using Core.Server.Common.Entities;
using Core.Server.Shared.Resources;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.Server.Application.Helpers
{
    public class ReflactionHelper : IReflactionHelper
    {
        private readonly List<Type> types;

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
                CreateResourceType = GetTypeWithPrefix<CreateResource>(resourceName),
                UpdateResourceType = GetTypeWithPrefix<UpdateResource>(resourceName),
                EntityType = GetTypeWithPrefix<Entity>(resourceName)
            };
        }

        public Type FillGenericType(Type genericType, ResourceBoundle resourceBoundle)
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

        public Type GetTypeByName(string typeName)
        {
            return types.FirstOrDefault(t => t.Name == typeName);
        }

        public IEnumerable<Type> GetDrivenTypesOf<T>()
        {
            return GetDrivenTypesOf(typeof(T));
        }

        public IEnumerable<Type> GetDrivenTypesOf(Type type)
        {
            return types.Where(t => t.BaseType == type);
        }

        public IEnumerable<Type> GetSameBaseTypeName(Type type)
        {
            return types.Where(t => t.BaseType?.Name == type.Name);
        }

        public IEnumerable<Type> GetTypesWithAttribute<TAttribute>()
            where TAttribute : Attribute
        {
            return types.Where(t => t.GetCustomAttribute<TAttribute>() != null);
        }

        public IEnumerable<Type> GetGenericTypesWithAttribute<TAttribute>()
            where TAttribute : Attribute
        {
            return GetTypesWithAttribute<TAttribute>().Where(t => t.IsGenericType);
        }

        public bool HasAttribute<TAttribute>(object obj)
            where TAttribute : Attribute
        {
            return obj.GetType().GetCustomAttributes<TAttribute>() != null;
        }
        public Type GetTypeWithPrefix<T>(string prefix)
        {
            var typeName = prefix + typeof(T).Name;
            return types.FirstOrDefault(t => t.Name == typeName);
        }

        public Type GetTypeWithName(string name)
        {
            return types.FirstOrDefault(t => t.Name == name);
        }

        public IEnumerable<PropertyInfo> GetProperiesWithAttribute<T, TAttribute>()
            where TAttribute : Attribute
        {
            return typeof(T).GetProperties()
                     .Where(p => p.GetCustomAttribute<TAttribute>() != null);
        }

        public void ForEachPropertyValue(object obj, Action<PropertyInfo, object> action)
        {
            foreach (var property in obj.GetType().GetProperties())
                action(property, property.GetValue(obj));
        }

        public string GetPrefixName(Type type)
        {
            return type.Name.Replace(type.BaseType.Name, string.Empty);
        }
    }
}
