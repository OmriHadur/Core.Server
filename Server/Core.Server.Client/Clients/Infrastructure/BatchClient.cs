using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;

namespace Core.Server.Client.Clients
{
    [Inject]
    public class BatchClient<TAlterResource, TResource>
        : ClientSender<TResource>,
          IBatchClient<TAlterResource, TResource>
        where TResource : Resource
    {

        public Task<ActionResult<IEnumerable<TResource>>> BatchCreate(TAlterResource[] resources)
        {
            return SentPostMany("batch", resources);
        }

        public Task<ActionResult<IEnumerable<TResource>>> BatchUpdate(TAlterResource[] resources)
        {
            return SentPutMany("batch", resources);
        }

        public Task<ActionResult<IEnumerable<string>>> BatchDelete(string[] ids)
        {
            return SendDeleteMany("batch", ids);
        }
    }
}