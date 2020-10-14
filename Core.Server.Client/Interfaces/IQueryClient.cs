using Core.Server.Client.Results;
using Core.Server.Shared.Query;
using Core.Server.Shared.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Server.Client.Interfaces
{
    public interface IQueryClient<TResource>
        where TResource : Resource
    {
        Task<ActionResult> Exists(string id);

        Task<ActionResult<IEnumerable<TResource>>> Get();

        Task<ActionResult<TResource>> Get(string id);

        Task<ActionResult<IEnumerable<TResource>>> Query(QueryResource query);
    }
}