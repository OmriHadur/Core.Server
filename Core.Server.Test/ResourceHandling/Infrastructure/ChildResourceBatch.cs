using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreation.Interfaces;
using Core.Server.Tests.ResourceCreators.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace Core.Server.Test.ResourcesCreators.Infrastructure
{
    [Inject]
    public class ChildResourceBatch<TCreateResource, TUpdateResource, TParentResource, TChildResource>
        : ResourceHandling<IChildBatchClient<TCreateResource, TUpdateResource, TParentResource>, TParentResource>
        , IChildResourceBatch<TCreateResource, TUpdateResource, TParentResource, TChildResource>
        where TCreateResource : ChildCreateResource
        where TUpdateResource : ChildUpdateResource
        where TParentResource : Resource
        where TChildResource : Resource
    {
        //[Dependency]
        //public IReflactionHelper ReflactionHelper;

        [Dependency]
        public IRandomResourceCreator<TCreateResource, TUpdateResource, TChildResource> RandomResourceCreator;

        [Dependency]
        public IResourceCreate<TChildResource> ChildResourceCreate;

        [Dependency]
        public IResourceCreate<TParentResource> ParentResourceCreate;

        public ActionResult<IEnumerable<TParentResource>> Create(int amount)
        {
            var createResources = new TCreateResource[amount];
            for (int i = 0; i < amount; i++)
            {
                createResources[i] = RandomResourceCreator.GetRandomCreateResource();
                createResources[i].ParentId = ParentResourceCreate.Create().Value.Id;
            }
            return Client.BatchCreate(createResources).Result;
        }

        public TChildResource[] GetChildResource(TParentResource parentResource)
        {
            var propertyInfos = parentResource.GetType().GetProperties();
            var propertyInfo = propertyInfos.FirstOrDefault(p => p.PropertyType == typeof(TChildResource[]));
            return (TChildResource[])(propertyInfo.GetValue(parentResource));
        }
    }
}