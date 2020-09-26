using Microsoft.Extensions.Configuration;
using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Core.Server.Tests.Utils;
using System;
using Unity;

namespace Core.Server.Tests.ResourceCreators
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
