using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using Core.Server.Test.ResourceCreation.Interfaces;
using System;
using System.Reflection;
using Unity;

namespace Core.Server.Test.ResourceCreation
{
    [Inject]
    public class RandomResourceCreator<TCreateResource, TUpdateResource, TResource>
        : IRandomResourceCreator<TCreateResource, TUpdateResource, TResource>
        where TCreateResource : CreateResource, new()
        where TUpdateResource : UpdateResource, new()
        where TResource: Resource
    {
        [Dependency]
        public IObjectRandomizer ObjectRandomizer;

        public TCreateResource GetRandomCreateResource()
        {
            var createResource = new TCreateResource();
            AddRandomValues(createResource);
            return createResource;
        }

        public TCreateResource GetRandomCreateResource(TResource existingResource)
        {
            var createResource = new TCreateResource();
            AddRandomValues(createResource, existingResource);
            return createResource;
        }

        public TUpdateResource GetRandomUpdateResource(TResource resource)
        {
            var updateResource = new TUpdateResource();
            AddRandomValues(updateResource);
            return updateResource;
        }

        protected virtual void AddRandomValues(TCreateResource createResource)
        {
            ObjectRandomizer.AddRandomValues(createResource);
        }

        protected virtual void AddRandomValues(TCreateResource createResource, TResource existingResource)
        {
            ObjectRandomizer.AddRandomValues(createResource);
        }

        protected virtual void AddRandomValues(TUpdateResource updateResource)
        {
            var randomProperty = ObjectRandomizer.GetRandomProperty<TUpdateResource>();
            if (randomProperty != null)
                ObjectRandomizer.SetRandomValue(updateResource, randomProperty);
        }
    }
}