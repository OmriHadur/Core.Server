using Core.Server.Common.Entities;
using Core.Server.Injection.Interfaces;
using Core.Server.Shared.Resources;
using System;
using System.Reflection;

namespace Core.Server.Injection.Unity
{
    public class ResourceBoundle
    {
        public Type TResource { get; private set; }
        public Type TEntity { get; private set; }
        public Type TAlterResource { get; private set; }

        protected string ResourceName { get; private set; }

        public ResourceBoundle(Type resourceType, IReflactionHelper reflactionHelper)
        {
            ResourceName = reflactionHelper.GetTypeName(resourceType, typeof(Resource));

            TResource = resourceType;
            TAlterResource = reflactionHelper.GetTypeByName(ResourceName + "AlterResource");
            TEntity = reflactionHelper.GetTypeWithPrefix<Entity>(ResourceName);
        }

        public Type GetTypeOf(string genericArgName)
        {
            foreach (var property in GetProperties())
                if (property.Name == genericArgName)
                    return (Type)property.GetValue(this);
            return null;
        }

        protected virtual PropertyInfo[] GetProperties()
        {
            return GetType().GetProperties();
        }

        public bool Contains(Type type)
        {
            return (TAlterResource == type ||
                TResource == type ||
                TEntity == type);
        }
    }
}
