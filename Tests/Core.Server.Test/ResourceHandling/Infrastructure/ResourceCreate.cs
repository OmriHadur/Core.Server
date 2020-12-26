using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using Core.Server.Test.ResourceCreation.Interfaces;
using Core.Server.Test.ResourceCreators.Interfaces;
using System.Linq;
using Unity;

namespace Core.Server.Test.ResourcesCreators.Infrastructure
{
    [Inject]
    public class ResourceCreate<TAlterResource, TResource>
        : ResourceHandling<IAlterClient<TAlterResource, TResource>, TResource>
        , IResourceCreate<TResource>
        where TResource : Resource
    {
        [Dependency]
        public IRandomResourceCreator<TAlterResource, TResource> RandomResourceCreator;

        [Dependency]
        public IResourceLookup<TResource> ResourceQuery;
        
        public ActionResult<TResource> Create()
        {
            var createResource = RandomResourceCreator.GetRandomCreateResource();
            var response = Client.Create(createResource).Result;
            if (response.IsSuccess)
                ResourceIdsHolder.Add<TResource>(response.Value.Id);
            return response;
        }

        public ActionResult Delete()
        {
            return Delete(ResourceIdsHolder.GetLast<TResource>());
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

        public TResource GetIfExist()
        {
            var id = ResourceIdsHolder.GetLast<TResource>();
            return id == null ? null : ResourceQuery.Get(id).Value;
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
