//using Core.Server.Injection.Interfaces;
//using Core.Server.Injection.Unity;
//using Core.Server.Shared.Resources;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Unity;

//namespace Core.Server.Injection.Reflaction
//{
//    public class ResourceBoundleHelper : IResourceBoundleHelper
//    {
//        private readonly Dictionary<Type, Type> interfaceToType;

//        [Dependency]
//        public IReflactionHelper ReflactionHelper;

//        public ResourceBoundleHelper()
//        {
//            interfaceToType = new Dictionary<Type, Type>();
//        }

//        public Type GetTypeGenericType(Type type, Type[] typeArgs, Type interTypeWithGeneric)
//        {
//            if (interfaceToType.ContainsKey(interTypeWithGeneric))
//                return interfaceToType[interTypeWithGeneric];
//            var typePartOfboundel = interTypeWithGeneric.GetGenericArguments().First();
//            var resourceBoundle = GetResourceBoundle(typePartOfboundel);
//            var typeGenericType = FillGenericType(type, resourceBoundle);
//            interfaceToType.Add(interTypeWithGeneric, typeGenericType);
//            return typeGenericType;
//        }

//        public Type FillGenericType(Type genericType, ResourceBoundle resourceBoundle)
//        {
//            var genericArguments = GetGenericArguments(genericType, resourceBoundle).ToArray();
//            return genericType.MakeGenericType(genericArguments);
//        }

//        private IEnumerable<Type> GetGenericArguments(Type genericType, ResourceBoundle resourceBoundle)
//        {
//            return genericType.GetGenericArguments()
//                .Select(ga => resourceBoundle.GetTypeOf(ga.Name));
//        }

//        public ResourceBoundle GetResourceBoundle(Type typePartOfboundel)
//        {
//            foreach (var resourceBoundle in GetAllResourcesBoundles())
//                if (resourceBoundle.Contains(typePartOfboundel))
//                    return resourceBoundle;
//            return null;
//        }

//        public IEnumerable<ResourceBoundle> GetAllResourcesBoundles()
//        {
//            return GetResourcesBoundles().Union(GetChildResourcesBoundles());
//        }
//        public IEnumerable<ResourceBoundle> GetResourcesBoundles()
//        {
//            var resourceTypes = GetDirectDrivenTypesOf<Resource>();

//            foreach (var resourceType in resourceTypes)
//                yield return new ResourceBoundle(resourceType, this);
//        }

//        public IEnumerable<ResourceBoundle> GetChildResourcesBoundles()
//        {
//            var resourceTypes = GetDirectDrivenTypesOf<ChildResource>();

//            foreach (var resourceType in resourceTypes)
//                yield return new ChildResourceBoundle(resourceType, this);
//        }

//        public IEnumerable<Type> GetDirectDrivenTypesOf<T>()
//        {
//            return GetDirectDrivenTypesOf(typeof(T));
//        }

//        public IEnumerable<Type> GetDirectDrivenTypesOf(Type type)
//        {
//            return ReflactionHelper.Types.Where(t => t.BaseType?.Name == type.Name);
//        }

//        public string GetTypeName(Type drivenType, Type subType)
//        {
//            return drivenType.Name.Replace(subType.Name, string.Empty);
//        }

//        public Type GetTypeWithPrefix<T>(string prefix)
//        {
//            var typeName = prefix + typeof(T).Name;
//            return ReflactionHelper.Types.FirstOrDefault(t => t.Name == typeName);
//        }
//    }
//}
