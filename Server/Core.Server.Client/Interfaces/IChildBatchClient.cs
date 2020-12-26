using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Server.Client.Interfaces
{
    public interface IChildBatchClient<TChildAlterResource, TResource>
        : IClientBase
        where TChildAlterResource : ChildAlterResource
        where TResource : Resource
    {
        Task<ActionResult<IEnumerable<TResource>>> BatchCreate(TChildAlterResource[] resources);
        Task<ActionResult<IEnumerable<TResource>>> BatchUpdate(TChildAlterResource[] resources);
        Task<ActionResult<IEnumerable<string>>> BatchDelete(ChildBatchDeleteResource childBatchDeleteResource);
    }
}