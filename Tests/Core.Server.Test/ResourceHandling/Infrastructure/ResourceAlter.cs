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
    public class ResourceAlter<TCreateResource, TUpdateResource, TResource>
        : ResourceHandling<IAlterClient<TCreateResource, TUpdateResource, TResource>, TResource>
        , IResourceAlter<TCreateResource, TUpdateResource, TResource>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
    {
        [Dependency]
        public IRandomResourceCreator<TCreateResource, TUpdateResource, TResource> RandomResourceCreator;

        [Dependency]
        public IResourceCreate<TResource> ResourceCreate;

        public ActionResult<TResource> Create(Action<TCreateResource> editFunc)
        {
            var createResource = RandomResourceCreator.GetRandomCreateResource();
            editFunc?.Invoke(createResource);
            return Create(createResource);
        }

        public ActionResult<TResource> Create(TCreateResource createResource)
        {
            var response = Client.Create(createResource).Result;
            if (response.IsSuccess)
                ResourceIdsHolder.Add<TResource>(response.Value.Id);
            return response;
        }

        public ActionResult<TResource> Replace(Action<TCreateResource> editFunc)
        {
            var resource = ResourceCreate.GetOrCreate();
            var updateResource = RandomResourceCreator.GetRandomCreateResource(resource);
            editFunc?.Invoke(updateResource);
            return Client.Replace(resource.Id, updateResource).Result;
        }

        public ActionResult<TResource> Update(Action<TUpdateResource> editFunc)
        {
            var resource = ResourceCreate.GetOrCreate();
            var updateResource = RandomResourceCreator.GetRandomUpdateResource(resource);
            editFunc?.Invoke(updateResource);
            return Client.Update(resource.Id, updateResource).Result;
        }
    }
}
