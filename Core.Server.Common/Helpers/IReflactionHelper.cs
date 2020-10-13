using Core.Server.Common;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Core.Server.Application.Helpers
{
    public interface IReflactionHelper
    {
        Type FillGenericType(Type genericType, ResourceBoundle resourceBoundle);
        IEnumerable<Type> GetDrivenTypesOf(Type type);
        IEnumerable<Type> GetDrivenTypesOf<T>();
        IEnumerable<Type> GetGenericTypesWithAttribute<TAttribute>() 
            where TAttribute : Attribute;
        IEnumerable<ResourceBoundle> GetResourcesBoundles();
        IEnumerable<Type> GetSameBaseTypeName(Type type);
        string GetTypeName(Type drivenType, Type subType);
        IEnumerable<Type> GetTypesWithAttribute<TAttribute>() 
            where TAttribute : Attribute;
        Type GetTypeWithPrefix<T>(string prefix);
        Type GetTypeWithName(string name);
        string GetPrefixName(Type type);

        bool HasAttribute<TAttribute>(object obj) 
            where TAttribute : Attribute;
        IEnumerable<PropertyInfo> GetProperiesWithAttribute<T, TAttribute>()
            where TAttribute : Attribute;

        void ForEachPropertyValue(object obj, Action<PropertyInfo, object> action);
    }
}