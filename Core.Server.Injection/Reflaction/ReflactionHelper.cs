using Core.Server.Common.Entities;
using Core.Server.Injection.Interfaces;
using Core.Server.Injection.Unity;
using Core.Server.Shared.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.Server.Injection.Reflaction
{
    public class ReflactionHelper : IReflactionHelper
    {
        private readonly List<Type> types;
        private readonly Dictionary<Type, Type> interfaceToType;

        public ReflactionHelper(string[] assembliesName)
        {
            interfaceToType = new Dictionary<Type, Type>();
            types = new List<Type>();
            foreach (Assembly assembly in GetAssemblies(assembliesName))
                types.AddRange(assembly.GetExportedTypes());
        }

        public IEnumerable<ResourceBoundle> GetResourcesBoundles()
        {
            var resourceTypes = GetDrivenTypesOf<Resource>();

            foreach (var resourceType in resourceTypes)
                yield return GetResourceBoundles(resourceType);
        }

        public Type FillGenericType(Type genericType, ResourceBoundle resourceBoundle)
        {
            var genericArguments = GetGenericArguments(genericType, resourceBoundle).ToArray();
            return genericType.MakeGenericType(genericArguments);
        }

        public Type GetClassForInterface<TInterface>()
        {
            return types.FirstOrDefault(t => HasInterafce<TInterface>(t));
        }

        private static bool HasInterafce<TInterface>(Type t)
        {
            return t.IsClass && t.IsGenericType && t.GetInterfaces().Any(i => i == typeof(TInterface));
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
            return types.Where(t => t.BaseType?.Name == type.Name);
        }

        public IEnumerable<Type> GetTypesWithAttribute<TAttribute>()
            where TAttribute : Attribute
        {
            return types.Where(t => t.GetCustomAttribute<TAttribute>() != null);
        }

        public IEnumerable<PropertyInfo> GetPropertiesWithAttribute<TAttribute>(object obj)
                where TAttribute : Attribute
        {
            return obj.GetType().GetProperties().Where(p => p.GetCustomAttribute<TAttribute>() != null);
        }

        public IEnumerable<Type> GetGenericTypesWithAttribute<TAttribute>()
            where TAttribute : Attribute
        {
            return GetTypesWithAttribute<TAttribute>().Where(t => t.IsGenericType);
        }

        public Type GetTypeWithPrefix<T>(string prefix)
        {
            var typeName = prefix + typeof(T).Name;
            return types.FirstOrDefault(t => t.Name == typeName);
        }

        public string GetPrefixName(Type type)
        {
            return type.Name.Replace(type.BaseType.Name, string.Empty);
        }

        protected virtual IEnumerable<Assembly> GetAssemblies(string[] assembliesName)
        {
            foreach (var assemblyName in assembliesName)
            {
                var path = $"{AppDomain.CurrentDomain.BaseDirectory}\\{assemblyName}.dll";
                yield return Assembly.LoadFrom(path);
            }
        }

        public Type GetTypeGenericType(Type type, Type[] typeArgs, Type interTypeWithGeneric)
        {
            if (interfaceToType.ContainsKey(interTypeWithGeneric))
                return interfaceToType[interTypeWithGeneric];
            var firstGen = interTypeWithGeneric.GetGenericArguments().First();
            var prefix = GetPrefixName(firstGen);
            var args = GetArguments(typeArgs, prefix).ToArray();
            var typeGenericType = type.MakeGenericType(args);
            interfaceToType.Add(interTypeWithGeneric, typeGenericType);
            return typeGenericType;
        }

        public IEnumerable<Type> GetDirectInterfaces(Type type)
        {
            var allInterfaces = new List<Type>();
            var childInterfaces = new List<Type>();

            foreach (var i in type.GetInterfaces())
            {
                allInterfaces.Add(i);
                foreach (var ii in i.GetInterfaces())
                    childInterfaces.Add(ii);
            }
            return allInterfaces.Except(childInterfaces);
        }

        private IEnumerable<Type> GetArguments(Type[] typeArgs, string prefix)
        {
            foreach (var type in typeArgs)
            {
                var typeName = prefix + type.Name.Substring(1);
                yield return GetTypeByName(typeName);
            }
        }

        private ResourceBoundle GetResourceBoundles(Type resourceType)
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

        private IEnumerable<Type> GetGenericArguments(Type genericType, ResourceBoundle resourceBoundle)
        {
            return genericType.GetGenericArguments()
                .Select(ga => resourceBoundle.GetSameBaseType(ga));
        }

        public bool IsSameType(TypeInfo parent, Type child)
        {
            if (parent.BaseType.Name != child.Name) return false;
            var firstGenericArgument = parent.BaseType.GetGenericArguments().FirstOrDefault();
            var firstControllerGenericArgument = child.GetGenericArguments().First();
            return firstGenericArgument == firstControllerGenericArgument;
        }
    }
}
