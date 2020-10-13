﻿using Core.Server.Common;
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
        string GetTypeName(Type drivenType, Type subType);
        Type GetTypeByName(string typeName);
        IEnumerable<Type> GetTypesWithAttribute<TAttribute>() 
            where TAttribute : Attribute;
        Type GetTypeWithPrefix<T>(string prefix);
        string GetPrefixName(Type type);

        bool IsSameType(TypeInfo parent, Type child);
    }
}