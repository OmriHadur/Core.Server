using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Server.Client.Clients
{
    public abstract class InnerRestClient<TCreateResource, TUpdateResource, TResource> :
        ClientBase, 
        IInnerRestClient<TCreateResource, TUpdateResource, TResource> 
        where TCreateResource : CreateResource
        where TUpdateResource: UpdateResource
        where TResource : Resource
    {
        public InnerRestClient(string apiRouteWithParentId) :
            base(apiRouteWithParentId)
        {
        }

        public Task<ActionResult<IEnumerable<TResource>>> Get(string parentId)
        {
            return GetAsync<IEnumerable<TResource>>(string.Format(ApiUrl,parentId));
        }

        public Task<ActionResult<TResource>> Get(string parentId, string id)
        {
            return GetAsync<TResource>(string.Format(ApiUrl, parentId) + id);
        }

        public Task<ActionResult<TResource>> Create(string parentId, TCreateResource resource)
        {
            return PostAsync<TResource>(string.Format(ApiUrl, parentId), resource);
        }

        public Task<ActionResult<TResource>> Update(string parentId, string id, TUpdateResource resource)
        {
            return PutAsync<TResource>(string.Format(ApiUrl, parentId) + id, resource);
        }

        public Task<ActionResult<TResource>> Delete(string parentId, string id)
        {
            return DeleteAsync<TResource>(string.Format(ApiUrl, parentId) + id);
        }
    }
}
