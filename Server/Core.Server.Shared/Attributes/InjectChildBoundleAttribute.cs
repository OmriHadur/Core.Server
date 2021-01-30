using System;

namespace Core.Server.Shared.Attributes
{
    public class InjectChildBoundleAttribute : InjectBoundleAttribute
    {
        public Type TParentResource { get; private set; }
        public Type TParentEntity { get; private set; }

        public InjectChildBoundleAttribute(Type childResource, Type childEntity, Type childAlterResource, Type parentResource, Type parentEntity)
            : base(childResource, childEntity, childAlterResource)
        {
            TParentResource = parentResource;
            TParentEntity = parentEntity;
        }
    }
}