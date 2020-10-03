using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Server.Web.Utils
{
    public class ResourceBoundle
    {
        public Type CreateResourceType { get; set; }
        public Type UpdateResourceType { get; set; }
        public Type ResourceType { get; set; }

        public ResourceBoundle(Type createResourceType, Type updateResourceType, Type resourceType)
        {
            CreateResourceType = createResourceType;
            UpdateResourceType = updateResourceType;
            ResourceType = resourceType;
        }

        public IEnumerable<Type> GetTypes()
        {
            yield return CreateResourceType;
            yield return UpdateResourceType;
            yield return ResourceType;
        }

        public Type GetSameBaseType(Type type)
        {
            return GetTypes().FirstOrDefault(t => t.BaseType == type.BaseType);
        }
    }
}
