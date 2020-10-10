using Core.Server.Common;
using System;
using System.Collections.Generic;

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
        Type GetTypeWithName<T>(string name);
        bool HasAttribute<TAttribute>(object obj) where TAttribute : Attribute;
    }
}