using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreation.Interfaces;
using System;
using Unity;

namespace Core.Server.Test.ResourceCreation
{
    public class RandomResourceCreator<TCreateResource, TUpdateResource, TResource>
        : IRandomResourceCreator<TCreateResource, TUpdateResource, TResource>
        where TCreateResource : CreateResource, new()
        where TUpdateResource : UpdateResource, new()
        where TResource: Resource
    {
        protected Random Random;

        [Dependency]
        public IObjectRandomizer ObjectRandomizer;

        public TCreateResource GetRandomCreateResource()
        {
            var createResource = new TCreateResource();
            AddRandomValues(createResource);
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

        protected virtual void AddRandomValues(TUpdateResource updateResource, TResource existingResource)
        {
            ObjectRandomizer.AddRandomValues(updateResource);
        }
    }
}