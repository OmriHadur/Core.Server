using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreation.Interfaces;
using Core.Server.Tests.ResourceCreators.Interfaces;
using System;
using Unity;

namespace Core.Server.Test.ResourcesCreators.Infrastructure
{
    public class ResourceAlter<TCreateResource, TUpdateResource, TResource>
        : ResourceHandling<IAlterClient<TCreateResource, TUpdateResource, TResource>, TResource>
        , IResourceAlter<TCreateResource, TUpdateResource, TResource>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
    {
        [Dependency]
        public IRandomResourceCreator<TCreateResource, TUpdateResource, TResource> RandomResourceCreator;

        public ActionResult<TResource> Create()
        {
            return Create(r => { });
        }

        public ActionResult<TResource> Create(Action<TCreateResource> editFunc)
        {
            var createResource = RandomResourceCreator.GetRandomCreateResource();
            editFunc(createResource);
            var response = Client.Create(createResource).Result;
            if (response.IsSuccess)
                ResourceIdsHolder.Add<TResource>(response.Value.Id);
            return response;
        }

        public ActionResult<TResource> Update()
        {
            var result = Create();
            if (result.IsFail)
                return result;
            var id = ResourceIdsHolder.GetLast<TResource>();
            var updateResource = RandomResourceCreator.GetRandomUpdateResource(result.Value);
            Client.CreateOrUpdate(id, updateResource);
        }

        public ActionResult<TResource> Update(Action<TUpdateResource> editFunc)
        {
            throw new NotImplementedException();
        }
    }
}
