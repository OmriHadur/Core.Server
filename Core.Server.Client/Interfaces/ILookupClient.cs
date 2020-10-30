using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Server.Client.Interfaces
{
    public interface ILookupClient<TResource>
        : IClientBase
        where TResource : Resource
    {
        Task<ActionResult> Exists(string id);

        Task<ActionResult<IEnumerable<TResource>>> Get();

        Task<ActionResult<IEnumerable<TResource>>> Get(IEnumerable<string> ids);

        Task<ActionResult<TResource>> Get(string id);
    }
}