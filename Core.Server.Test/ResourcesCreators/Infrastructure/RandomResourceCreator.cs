using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Core.Server.Tests.Utils;
using System;
using Unity;

namespace Core.Server.Test.ResourcesCreators.Infrastructure
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
            ObjectRandomizer.AddRandomValues(createResource);
            return createResource;
        }

        public TUpdateResource GetRandomUpdateResource()
        {
            var updateResource = new TUpdateResource();
            ObjectRandomizer.AddRandomValues(updateResource);
            return updateResource;
        }
    }
}