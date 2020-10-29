using Core.Server.Shared.Resources;
using System;
using System.Linq;
using System.Reflection;

namespace Core.Server.Application.Query
{
    public abstract class QueringBase
    {
        protected PropertyInfo GetPropertyInfo<TResource>(string propertyName)
            where TResource : Resource
        {
            return GetPropertyInfo(propertyName, typeof(TResource));
        }

        protected PropertyInfo GetPropertyInfo(string propertyName, Type type)
        {
            return type.GetProperties().FirstOrDefault(p => p.Name.ToLower() == propertyName);
        }

        protected string GetPropertyName<TResource>(string propertyName)
            where TResource : Resource
        {
            return GetPropertyInfo<TResource>(propertyName).Name;
        }
    }
}