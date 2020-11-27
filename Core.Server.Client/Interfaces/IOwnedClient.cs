using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Server.Client.Interfaces
{
    public interface IOwnedClient<TResource>
        : IClientBase
        where TResource : Resource
    {
        Task<ActionResult<IEnumerable<TResource>>> GetAll();

        Task<ActionResult> Any();

        Task<ActionResult> Reassign(ReassginResource reassginResource);
    }
}