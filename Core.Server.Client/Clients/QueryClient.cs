using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Shared.Query;
using Core.Server.Shared.Resources;

namespace Core.Server.Client.Clients
{
    public abstract class QueryClient<TResource>
        : ClientSender<TResource>,
          IQueryClient<TResource>
        where TResource : Resource
    {
        public Task<ActionResult> Exists(string id)
        {
            return SendHead(id);
        }

        public Task<ActionResult<IEnumerable<TResource>>> Get()
        {
            return SendGetMany();
        }

        public Task<ActionResult<IEnumerable<TResource>>> Get(string[] ids)
        {
            return SentPostMany("ids", ids);
        }

        public Task<ActionResult<TResource>> Get(string id)
        {
            return SendGet(id);
        }

        public Task<ActionResult<IEnumerable<TResource>>> Query(QueryResource query)
        {
            return SentPostMany("query", query);
        }
    }
}