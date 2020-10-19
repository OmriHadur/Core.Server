using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Injection.Attributes;
using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreation.Interfaces;
using Core.Server.Tests.ResourceCreators.Interfaces;
using System;
using System.Linq;
using Unity;

namespace Core.Server.Test.ResourcesCreators.Infrastructure
{
    [Inject]
    public class ResourceCreate<TCreateResource, TUpdateResource, TResource>
        : ResourceHandling<IAlterClient<TCreateResource, TUpdateResource, TResource>, TResource>
        , IResourceCreate<TResource>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
    {
        [Dependency]
        public IRandomResourceCreator<TCreateResource, TUpdateResource, TResource> RandomResourceCreator;

        [Dependency]
        public IResourceQuery<TResource> ResourceQuery;
        
        public ActionResult<TResource> Create()
        {
            var createResource = RandomResourceCreator.GetRandomCreateResource();
            var response = Client.Create(createResource).Result;
            if (response.IsSuccess)
                ResourceIdsHolder.Add<TResource>(response.Value.Id);
            return response;
        }

        public ActionResult Delete(string id)
        {
            var response = Client.Delete(id).Result;
            if (response is OkResult)
                ResourceIdsHolder.Remove<TResource>(id);
            return response;
        }

        public void DeleteAll()
        {
            var ids = ResourceIdsHolder.GetAll<TResource>().ToList();
            foreach (var id in ids)
                Delete(id);
        }

        public TResource GetOrCreate()
        {
            if (ResourceIdsHolder.IsEmpty<TResource>())
                return Create().Value;
            else
            {
                var id = ResourceIdsHolder.GetLast<TResource>();
                return ResourceQuery.Get(id).Value;
            }
        }
    }
}
