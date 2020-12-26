using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Server.Client.Interfaces
{
    public interface IBatchClient<TAlterResource, TResource>
        : IClientBase
        where TResource : Resource
    {
        Task<ActionResult<IEnumerable<TResource>>> BatchCreate(TAlterResource[] resources);

        Task<ActionResult<IEnumerable<TResource>>> BatchUpdate(TAlterResource[] resources);

        Task<ActionResult<IEnumerable<string>>> BatchDelete(string[] ids);
    }
}