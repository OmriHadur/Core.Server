using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Server.Client.Interfaces
{
    public interface IChildBatchClient<TCreateResource, TUpdateResource, TResource>
        : IClientBase
        where TCreateResource : ChildCreateResource
        where TUpdateResource : ChildUpdateResource
        where TResource : Resource
    {
        Task<ActionResult<IEnumerable<TResource>>> BatchCreate(TCreateResource[] resources);
        Task<ActionResult<IEnumerable<TResource>>> BatchUpdate(TUpdateResource[] resources);
        Task<ActionResult<IEnumerable<string>>> BatchDelete(ChildBatchDeleteResource childBatchDeleteResource);
    }
}