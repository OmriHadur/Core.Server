using Core.Server.Common.Entities;
using Core.Server.Injection.Interfaces;
using Core.Server.Shared.Resources;
using System;
using System.Reflection;

namespace Core.Server.Injection.Unity
{
    public class ChildResourceBoundle : ResourceBoundle
    {
        public Type TParentResource { get; private set; }
        public Type TParentEntity { get; private set; }

        public Type TChildEntity => TEntity;
        public Type TChildResource => TResource;
        public Type TChildAlterResource => TAlterResource;

        public ChildResourceBoundle(Type resourceType, IReflactionHelper reflactionHelper)
            : base(resourceType, reflactionHelper)
        {
            var parentName = GetFirstWord(ResourceName);
            TParentEntity = reflactionHelper.GetTypeWithPrefix<Entity>(parentName);
            TParentResource = reflactionHelper.GetTypeWithPrefix<Resource>(parentName);
        }

        protected override PropertyInfo[] GetProperties()
        {
            return GetType().GetProperties();
        }

        private string GetFirstWord(string str)
        {
            for (int i = 1; i < str.Length; i++)
                if (char.IsUpper(str[i]))
                    return str.Substring(0, i);
            return str;
        }
    }
}
