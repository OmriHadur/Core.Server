using System;

namespace Core.Server.Shared.Attributes
{
    public class InjectBoundleAttribute : Attribute
    {
        public Type TResource { get; private set; }
        public Type TEntity { get; private set; }
        public Type TAlterResource { get; private set; }

        public InjectBoundleAttribute(Type resource, Type entity, Type alterResource)
        {
            TResource = resource;
            TEntity = entity;
            TAlterResource = alterResource;
        }
    }
}