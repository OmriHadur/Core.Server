using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using System.Net.Http;
using System.Threading.Tasks;

namespace Core.Server.Client.Clients
{
    [Inject]
    public class ChildAlterClient<TCreateResource, TUpdateResource, TParentResource, TChildResource>
        : ChildClientSender<TParentResource,TChildResource>,
          IChildAlterClient<TCreateResource, TUpdateResource, TParentResource>
        where TCreateResource : ChildCreateResource
        where TUpdateResource : ChildUpdateResource
        where TParentResource : Resource
        where TChildResource : Resource
    {
        public Task<ActionResult<TParentResource>> Create(TCreateResource resource)
        {
            return SentPost(resource);
        }

        public Task<ActionResult<TParentResource>> Delete(string id, ChildDeleteResource childResource)
        {
            return SendMethod<TParentResource>(id, HttpMethod.Delete, childResource);
        }

        public Task<ActionResult<TParentResource>> DeleteAll(ChildDeleteResource childResource)
        {
            return SendMethod<TParentResource>(HttpMethod.Delete, childResource);
        }

        public Task<ActionResult<TParentResource>> Replace(string id, TCreateResource resource)
        {
            return SendMethod<TParentResource>(id, HttpMethod.Put, resource);
        }

        public Task<ActionResult<TParentResource>> Update(string id, TUpdateResource resource)
        {
            return SendMethod<TParentResource>(id, HttpMethod.Patch, resource);
        }
    }
}
