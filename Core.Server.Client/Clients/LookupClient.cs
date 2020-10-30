using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Injection.Attributes;
using Core.Server.Shared.Resources;

namespace Core.Server.Client.Clients
{
    [Inject]
    public class LookupClient<TResource>
        : ClientSender<TResource>,
          ILookupClient<TResource>
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

        public Task<ActionResult<IEnumerable<TResource>>> Get(IEnumerable<string> ids)
        {
            return SentPostMany("ids", ids);
        }

        public Task<ActionResult<TResource>> Get(string id)
        {
            return SendGet(id);
        }
    }
}