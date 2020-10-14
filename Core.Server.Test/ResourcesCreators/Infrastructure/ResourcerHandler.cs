using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreators.Interfaces;
using System.Collections.Generic;
using Unity;

namespace Core.Server.Test.ResourcesCreators.Infrastructure
{
    public class ResourcerHandler<TCreateResource, TUpdateResource, TResource>
        : IResourceHandler<TResource>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
    {
        [Dependency]
        public IResourceIdsHolder<TResource> ResourceIdsHolder;

        [Dependency]
        public IQueryClient<TResource> QueryClient;

        [Dependency]
        public IAlterClient<TCreateResource, TUpdateResource, TResource> AlterClient;

        [Dependency]
        public IRandomResourceCreator<TCreateResource, TUpdateResource> RandomResourceCreator;

        public ActionResult<TResource> Create()
        {
            var createResource = RandomResourceCreator.GetRandomCreateResource();
            var result = AlterClient.Create(createResource).Result;
            if (result.IsSuccess)
                ResourceIdsHolder.Add(result.Value.Id);
            return result;
        }

        public ActionResult Delete(string id)
        {
            var result = AlterClient.Delete(id).Result;
            if (result.IsSuccess)
                ResourceIdsHolder.Remove(id);
            return result;
        }

        public void DeleteAll()
        {
            foreach (var id in ResourceIdsHolder.GetAll())
                Delete(id);
        }

        public TResource Get()
        {
            string resourceId;
            if (ResourceIdsHolder.IsEmpty())
                resourceId = Create().Value?.Id;
            else
                resourceId = ResourceIdsHolder.GetLast();
            return QueryClient.Get(resourceId).Result.Value;
        }

        public ActionResult<TResource> Get(string id)
        {
            return QueryClient.Get(id).Result;
        }

        public IEnumerable<TResource> GetAll()
        {
            foreach (var id in ResourceIdsHolder.GetAll())
                yield return Get(id).Value;
        }
    }
}
