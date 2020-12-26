using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using Core.Server.Test.ResourceCreation.Interfaces;
using System;
using System.Reflection;
using Unity;

namespace Core.Server.Test.ResourceCreation
{
    [Inject]
    public class RandomResourceCreator<TAlterResource, TResource>
        : IRandomResourceCreator<TAlterResource, TResource>
        where TAlterResource : new()
        where TResource: Resource
    {
        [Dependency]
        public IObjectRandomizer ObjectRandomizer;

        public TAlterResource GetRandomCreateResource()
        {
            var createResource = new TAlterResource();
            AddRandomCreateValues(createResource);
            return createResource;
        }

        public TAlterResource GetRandomCreateResource(TResource existingResource)
        {
            var createResource = new TAlterResource();
            AddRandomValues(createResource, existingResource);
            return createResource;
        }

        public TAlterResource GetRandomUpdateResource(TResource existingResource)
        {
            var createResource = new TAlterResource();
            AddRandomValues(createResource, existingResource);
            return createResource;
        }

        protected virtual void AddRandomCreateValues(TAlterResource createResource)
        {
            ObjectRandomizer.AddRandomValues(createResource);
        }


        protected virtual void AddRandomValues(TAlterResource createResource, TResource existingResource)
        {
            ObjectRandomizer.AddRandomValues(createResource);
        }

        protected virtual void AddRandomUpdateValues(TAlterResource updateResource)
        {
            var randomProperty = ObjectRandomizer.GetRandomProperty<TAlterResource>();
            if (randomProperty != null)
                ObjectRandomizer.SetRandomValue(updateResource, randomProperty);
        }
    }
}