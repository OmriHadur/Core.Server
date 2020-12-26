using Core.Server.Injection.Unity;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Core.Server.Injection.Interfaces
{
    public interface IReflactionHelper
    {
        Type GetClassForInterface<TInterface>();
        Type FillGenericType(Type genericType, ResourceBoundle resourceBoundle);
        IEnumerable<Type> GetDrivenTypesOf(Type type);
        IEnumerable<Type> GetGenericTypesWithAttribute<TAttribute>() 
            where TAttribute : Attribute;
        IEnumerable<ResourceBoundle> GetAllResourcesBoundles();
        IEnumerable<ResourceBoundle> GetResourcesBoundles();
        string GetTypeFullName(Type type);
        IEnumerable<ResourceBoundle> GetChildResourcesBoundles();
        string GetTypeName(Type drivenType, Type subType);
        Type GetTypeByName(string typeName);
        IEnumerable<Type> GetTypesWithAttribute<TAttribute>() 
            where TAttribute : Attribute;
        IEnumerable<PropertyInfo> GetPropertiesWithAttribute<TAttribute>(object obj)
            where TAttribute : Attribute;
        Type GetTypeWithPrefix<T>(string prefix);
        string GetPrefixName(Type type);
        IEnumerable<Type> GetDirectInterfaces(Type type);
        bool IsSameType(TypeInfo parent, Type child);
        Type GetTypeGenericType(Type type, Type[] typeArgs, Type interTypeWithGeneric);
        bool IsDrivenType(Type type, Type baseType);
        T GetValueOf<T>(object objWithArrayOfT);
    }
}