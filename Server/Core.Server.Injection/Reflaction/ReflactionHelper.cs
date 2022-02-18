using Core.Server.Injection.Interfaces;
using Core.Server.Injection.Unity;
using Core.Server.Shared.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Core.Server.Injection.Reflaction
{
    public class ReflactionHelper : IReflactionHelper
    {
        private readonly List<Type> types;
        private readonly Dictionary<Type, Type> interfaceToType;

        public ReflactionHelper(string[] assembliesPrefix)
        {
            interfaceToType = new Dictionary<Type, Type>();
            types = new List<Type>();
            foreach (Assembly assembly in GetAssemblies(assembliesPrefix))
                types.AddRange(assembly.GetExportedTypes());
        }

        public IEnumerable<ResourceBoundle> GetAllResourcesBoundles()
        {
            return GetResourcesBoundles().Union(GetChildResourcesBoundles());
        }
        public IEnumerable<ResourceBoundle> GetResourcesBoundles()
        {
            var resourceTypes = GetDrivenTypesOf<Resource>().Where(t =>
                t != typeof(ChildResource) &&
                t != typeof(NamedResource)
            );

            foreach (var resourceType in resourceTypes)
                yield return new ResourceBoundle(resourceType, this);
        }

        public IEnumerable<ResourceBoundle> GetChildResourcesBoundles()
        {
            var resourceTypes = GetDirectDrivenTypesOf<ChildResource>();

            foreach (var resourceType in resourceTypes)
                yield return new ChildResourceBoundle(resourceType, this);
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

        public string GetTypeFullName(Type type)
        {
            var assemblyFullName = type.Assembly.FullName;
            assemblyFullName = assemblyFullName.Substring(0, assemblyFullName.IndexOf(','));
            return type.FullName + "," + assemblyFullName;
        }

        public Type GetTypeByName(string typeName)
        {
            return types.FirstOrDefault(t => t.Name == typeName);
        }

        public IEnumerable<Type> GetDirectDrivenTypesOf<T>()
        {
            return GetDirectDrivenTypesOf(typeof(T));
        }

        public IEnumerable<Type> GetDrivenTypesOf<T>()
        {
            return GetDrivenTypesOf(typeof(T));
        }

        public IEnumerable<Type> GetDrivenTypesOf(Type type)
        {
            return types.Where(t => IsDrivenType(t, type));
        }

        public IEnumerable<Type> GetDirectDrivenTypesOf(Type type)
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

        protected virtual IEnumerable<Assembly> GetAssemblies(string[] assembliesPrefixes)
        {
            foreach (var assembliesPrefix in assembliesPrefixes)
            {
                var assembliesPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, assembliesPrefix + "*.dll");
                foreach (var assembliesPath in assembliesPaths)
                    yield return Assembly.LoadFrom(assembliesPath);
            }
        }

        public Type GetTypeGenericType(Type type, Type[] typeArgs, Type interTypeWithGeneric)
        {
            if (interfaceToType.ContainsKey(interTypeWithGeneric))
                return interfaceToType[interTypeWithGeneric];
            var typePartOfboundel = interTypeWithGeneric.GetGenericArguments().First();
            var resourceBoundle = GetResourceBoundle(typePartOfboundel);
            var typeGenericType = FillGenericType(type, resourceBoundle);
            interfaceToType.Add(interTypeWithGeneric, typeGenericType);
            return typeGenericType;
        }

        private ResourceBoundle GetResourceBoundle(Type typePartOfboundel)
        {
            foreach (var resourceBoundle in GetAllResourcesBoundles())
                if (resourceBoundle.Contains(typePartOfboundel))
                    return resourceBoundle;
            return null;
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

        private IEnumerable<Type> GetGenericArguments(Type genericType, ResourceBoundle resourceBoundle)
        {
            return genericType.GetGenericArguments()
                .Select(ga => resourceBoundle.GetTypeOf(ga.Name));
        }

        public bool IsSameType(TypeInfo parent, Type child)
        {
            if (parent.BaseType.Name != child.Name) return false;
            var firstGenericArgument = parent.BaseType.GetGenericArguments().FirstOrDefault();
            var firstControllerGenericArgument = child.GetGenericArguments().First();
            return firstGenericArgument == firstControllerGenericArgument;
        }

        public bool IsDrivenType(Type type, Type baseType)
        {
            do
            {
                type = type.BaseType;
                if (type?.Name == baseType.Name)
                    return true;

            } while (type != null);
            return false;
        }
        public T GetValueOf<T>(object obj)
        {
            var prop = obj.GetType().GetProperties().FirstOrDefault(p => p.PropertyType == typeof(T));
            return (T)(prop.GetValue(obj));
        }
    }
}
