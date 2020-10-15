using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreation.Interfaces;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Core.Server.Tests.Utils;
using System;
using Unity;

namespace Core.Server.Test.ResourceCreation
{
    public class RandomResourceCreator<TCreateResource, TUpdateResource>
        : IRandomResourceCreator<TCreateResource, TUpdateResource>
        where TCreateResource : CreateResource, new()
        where TUpdateResource : UpdateResource, new()
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

        public TUpdateResource GetRandomUpdateResource()
        {
            var updateResource = new TUpdateResource();
            AddRandomValues(updateResource);
            return updateResource;
        }

        protected virtual void AddRandomValues(TCreateResource createResource)
        {
            ObjectRandomizer.AddRandomValues(createResource);
        }

        protected virtual void AddRandomValues(TUpdateResource updateResource)
        {
            ObjectRandomizer.AddRandomValues(updateResource);
        }
    }
}