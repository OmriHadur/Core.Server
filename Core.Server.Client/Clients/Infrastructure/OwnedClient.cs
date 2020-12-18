using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;

namespace Core.Server.Client.Clients
{
    [Inject]
    public class OwnedClient<TResource>
        : ClientSender<TResource>,
          IOwnedClient<TResource>
        where TResource : Resource
    {
        public Task<ActionResult> Any()
        {
            return SendHead("owned");
        }

        public Task<ActionResult<IEnumerable<TResource>>> GetAll()
        {
            return SendGetMany("owned");
        }

        public Task<ActionResult> Assign(string resourceId)
        {
            return SentPostNoResource("assign", resourceId);
        }

        public Task<ActionResult> Reassign(ReassginResource reassginResource)
        {
            return SentPostNoResource("reassign", reassginResource);
        }
    }
}