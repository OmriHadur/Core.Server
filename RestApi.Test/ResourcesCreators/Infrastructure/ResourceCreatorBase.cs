using Microsoft.Extensions.Configuration;
using RestApi.Shared.Resources;
using RestApi.Tests.RestResourcesCreators.Interfaces;
using RestApi.Tests.Utils;
using System;
using Unity;

namespace RestApi.Tests.RestResourcesCreators
{
    public class ResourceCreatorBase
    {
        protected Random Random;

        [Dependency]
        public IResourcesHolder ResourcesHolder;

        [Dependency]
        public IObjectRandomizer ObjectRandomizer;

        [Dependency]
        public ITokenHandler TokenHandler;

        [Dependency]
        public IConfigHandler ConfigHandler;

        public ResourceCreatorBase()
        {
            Random = new Random();
        }

        protected TFResource GetResource<TFResource>()
            where TFResource : Resource
        {
            return ResourcesHolder.GetLastOrCreate<TFResource>().Value;
        }
        protected string GetResourceId<TFResource>()
            where TFResource : Resource
        {
            return ResourcesHolder.GetResourceId<TFResource>();
        }
    }
}
