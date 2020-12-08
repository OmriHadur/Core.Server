using Core.Server.Common.Entities;
using Core.Server.Injection.Interfaces;
using Core.Server.Shared.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Server.Injection.Unity
{
    public class ResourceBoundle : List<Type>
    {
        protected string ResourceName;

        public ResourceBoundle(Type resourceType, IReflactionHelper reflactionHelper)
        {
            ResourceName = reflactionHelper.GetTypeName(resourceType, typeof(Resource));

            Add(resourceType);
            Add(reflactionHelper.GetTypeWithPrefix<CreateResource>(ResourceName));
            Add(reflactionHelper.GetTypeWithPrefix<UpdateResource>(ResourceName));
            Add(reflactionHelper.GetTypeWithPrefix<Entity>(ResourceName));
        }

        public Type GetSameBaseType(Type type)
        {
            return this.FirstOrDefault(
                t => t.BaseType == type.BaseType ||
                t.BaseType.BaseType == type.BaseType);
        }
    }
}
