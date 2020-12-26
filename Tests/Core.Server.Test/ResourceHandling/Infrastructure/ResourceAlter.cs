using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using Core.Server.Test.ResourceCreation.Interfaces;
using Core.Server.Test.ResourceCreators.Interfaces;
using System;
using Unity;

namespace Core.Server.Test.ResourcesCreators.Infrastructure
{
    [Inject]
    public class ResourceAlter<TAlterResource, TResource>
        : ResourceHandling<IAlterClient<TAlterResource, TResource>, TResource>
        , IResourceAlter<TAlterResource, TResource>
        where TResource : Resource
    {
        [Dependency]
        public IRandomResourceCreator<TAlterResource, TResource> RandomResourceCreator;

        [Dependency]
        public IResourceCreate<TResource> ResourceCreate;

        public ActionResult<TResource> Create(Action<TAlterResource> editFunc)
        {
            var createResource = RandomResourceCreator.GetRandomCreateResource();
            editFunc?.Invoke(createResource);
            return Create(createResource);
        }

        public ActionResult<TResource> Create(TAlterResource createResource)
        {
            var response = Client.Create(createResource).Result;
            if (response.IsSuccess)
                ResourceIdsHolder.Add<TResource>(response.Value.Id);
            return response;
        }

        public ActionResult<TResource> Replace(Action<TAlterResource> editFunc)
        {
            var resource = ResourceCreate.GetOrCreate();
            var updateResource = RandomResourceCreator.GetRandomCreateResource(resource);
            editFunc?.Invoke(updateResource);
            return Client.Replace(resource.Id, updateResource).Result;
        }

        public ActionResult<TResource> Update(Action<TAlterResource> editFunc)
        {
            var resource = ResourceCreate.GetOrCreate();
            var updateResource = RandomResourceCreator.GetRandomUpdateResource(resource);
            editFunc?.Invoke(updateResource);
            return Client.Update(resource.Id, updateResource).Result;
        }
    }
}
