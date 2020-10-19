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
        IEnumerable<Type> GetDrivenTypesOf<T>();
        IEnumerable<Type> GetGenericTypesWithAttribute<TAttribute>() 
            where TAttribute : Attribute;
        IEnumerable<ResourceBoundle> GetResourcesBoundles();
        string GetTypeName(Type drivenType, Type subType);
        Type GetTypeByName(string typeName);
        IEnumerable<Type> GetTypesWithAttribute<TAttribute>() 
            where TAttribute : Attribute;
        Type GetTypeWithPrefix<T>(string prefix);
        string GetPrefixName(Type type);

        bool IsSameType(TypeInfo parent, Type child);
        Type GetTypeGenericType(Type type, Type[] typeArgs, Type interTypeWithGeneric);
    }
}