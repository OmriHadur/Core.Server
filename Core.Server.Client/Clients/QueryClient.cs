using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Injection.Attributes;
using Core.Server.Shared.Query;
using Core.Server.Shared.Resources;

namespace Core.Server.Client.Clients
{
    [Inject]
    public class QueryClient<TResource>
        : ClientSender<TResource>,
          IQueryClient<TResource>
        where TResource : Resource
    {
        public Task<ActionResult<IEnumerable<TResource>>> Query(QueryResource queryResource)
        {
            return SentPostMany("query", queryResource);
        }
    }
}