using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using System.Net.Http;
using System.Threading.Tasks;

namespace Core.Server.Client.Clients
{
    [Inject]
    public class ChildAlterClient<TChildAlterResource, TParentResource, TChildResource>
        : ChildClientSender<TParentResource, TChildResource>,
          IChildAlterClient<TChildAlterResource, TParentResource>
        where TChildAlterResource : ChildAlterResource
        where TParentResource : Resource
        where TChildResource : Resource
    {
        public Task<ActionResult<TParentResource>> Create(TChildAlterResource resource)
        {
            return SentPost(resource);
        }

        public Task<ActionResult<TParentResource>> Delete(string id, ChildAlterResource childResource)
        {
            return SendMethod<TParentResource>(id, HttpMethod.Delete, childResource);
        }

        public Task<ActionResult<TParentResource>> DeleteAll(ChildAlterResource childResource)
        {
            return SendMethod<TParentResource>(HttpMethod.Delete, childResource);
        }

        public Task<ActionResult<TParentResource>> Replace(string id, TChildAlterResource resource)
        {
            return SendMethod<TParentResource>(id, HttpMethod.Put, resource);
        }

        public Task<ActionResult<TParentResource>> Update(string id, TChildAlterResource resource)
        {
            return SendMethod<TParentResource>(id, HttpMethod.Patch, resource);
        }
    }
}
