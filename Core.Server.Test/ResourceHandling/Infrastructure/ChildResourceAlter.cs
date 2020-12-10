using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreation.Interfaces;
using Core.Server.Tests.ResourceCreators.Interfaces;
using System;
using Unity;

namespace Core.Server.Test.ResourcesCreators.Infrastructure
{
    [Inject]
    public class ChildResourceAlter<TCreateResource, TUpdateResource, TParentResource, TChildResource>
        : ResourceHandling<IChildAlterClient<TCreateResource, TUpdateResource, TParentResource>, TParentResource>
        , IChildResourceAlter<TCreateResource, TUpdateResource, TParentResource>
        where TCreateResource : ChildCreateResource
        where TUpdateResource : ChildUpdateResource
        where TParentResource : Resource
        where TChildResource : Resource
    {
        [Dependency]
        public IRandomResourceCreator<TCreateResource, TUpdateResource, TChildResource> RandomResourceCreator;

        [Dependency]
        public IResourceCreate<TChildResource> ChildResourceCreate;

        [Dependency]
        public IResourceCreate<TParentResource> ParentResourceCreate;

        public ActionResult<TParentResource> Create(Action<TCreateResource> editFunc = null)
        {
            var parent = ParentResourceCreate.GetOrCreate();
            var createResource = RandomResourceCreator.GetRandomCreateResource();
            createResource.ParentId = parent.Id;
            editFunc?.Invoke(createResource);
            return Create(createResource);
        }

        public ActionResult<TParentResource> Create(TCreateResource createResource)
        {
            return Client.Create(createResource).Result;
        }

        public ActionResult<TParentResource> Replace(string childId, Action<TCreateResource> editFunc = null)
        {
            var parent = ParentResourceCreate.GetOrCreate();
            var createResource = RandomResourceCreator.GetRandomCreateResource();
            var updateResource = RandomResourceCreator.GetRandomCreateResource(resource);
            editFunc?.Invoke(updateResource);
            return Client.Replace(resource.Id, updateResource).Result;
        }

        public ActionResult<TParentResource> Update(string childId, Action<TUpdateResource> editFunc = null)
        {
            throw new NotImplementedException();
        }
    }
}
