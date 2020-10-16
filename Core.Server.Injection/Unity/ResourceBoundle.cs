using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Server.Injection.Unity
{
    public class ResourceBoundle
    {
        public Type CreateResourceType { get; set; }
        public Type UpdateResourceType { get; set; }
        public Type ResourceType { get; set; }
        public Type EntityType { get; set; }

        public IEnumerable<Type> GetTypes()
        {
            yield return CreateResourceType;
            yield return UpdateResourceType;
            yield return ResourceType;
            yield return EntityType;
        }

        public Type GetSameBaseType(Type type)
        {
            return GetTypes().FirstOrDefault(t => t.BaseType == type.BaseType);
        }
    }
}
