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
        public Type TCreateResource { get; private set; }
        public Type TUpdateResource { get; private set; }

        public string ResourceName { get; private set; }

        public ResourceBoundle(Type resourceType, IReflactionHelper reflactionHelper)
        {
            ResourceName = reflactionHelper.GetTypeName(resourceType, typeof(Resource));

            TResource = resourceType;
            TCreateResource = reflactionHelper.GetTypeWithPrefix<CreateResource>(ResourceName);
            TUpdateResource = reflactionHelper.GetTypeWithPrefix<UpdateResource>(ResourceName);
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
            return (TCreateResource == type || 
                TResource == type || 
                TEntity == type || 
                TUpdateResource == type);
        }
    }
}
