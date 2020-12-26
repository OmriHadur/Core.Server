using Core.Server.Client.Results;
using Core.Server.Shared.Query;
using Core.Server.Shared.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Server.Client.Interfaces
{
    public interface IQueryClient<TResource>
        : IClientBase
        where TResource : Resource
    {
        Task<ActionResult<IEnumerable<TResource>>> Query(QueryResource queryResource);
    }
}