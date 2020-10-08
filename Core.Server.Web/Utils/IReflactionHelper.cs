using System;
using System.Collections.Generic;

namespace Core.Server.Web.Utils
{
    public interface IReflactionHelper
    {
        IEnumerable<Type> GetDrivenTypesOf<T>();

        IEnumerable<Type> GetDrivenTypesOf(Type type);

        IEnumerable<Type> GetSameBaseTypeName(Type type);

        IEnumerable<ResourceBoundle> GetResourcesBoundles();

        string GetTypeName(Type drivenType, Type subType);

        Type GetTypeWithName<T>(string name);

        Type FillGenericType(Type genericType, ResourceBoundle resourceBoundle);

        IEnumerable<Type> GetTypesWithAttribute<TAttribute>()
             where TAttribute : Attribute;

        IEnumerable<Type> GetGenericTypesWithAttribute<TAttribute>()
            where TAttribute : Attribute;
        
    }
}