using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Core.Server.Client.Clients
{
    [Inject]
    public class ChildBatchClient<TChildAlterResource, TParentResource, TChildResource>
        : ChildClientSender<TParentResource, TChildResource>,
          IChildBatchClient<TChildAlterResource, TParentResource>
        where TChildAlterResource : ChildAlterResource
        where TParentResource : Resource
        where TChildResource : Resource
    {
        public Task<ActionResult<IEnumerable<TParentResource>>> BatchCreate(TChildAlterResource[] resources)
        {
            return SendMethod<IEnumerable<TParentResource>>("batch", HttpMethod.Post, resources);
        }

        public Task<ActionResult<IEnumerable<TParentResource>>> BatchUpdate(TChildAlterResource[] resources)
        {
            return SendMethod<IEnumerable<TParentResource>>("batch", HttpMethod.Put, resources);
        }

        public Task<ActionResult<IEnumerable<string>>> BatchDelete(ChildBatchDeleteResource childBatchDeleteResource)
        {
            return SendMethod<IEnumerable<string>>("batch", HttpMethod.Delete, childBatchDeleteResource);
        }
    }
}
