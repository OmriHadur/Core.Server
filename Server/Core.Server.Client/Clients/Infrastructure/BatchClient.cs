using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;

namespace Core.Server.Client.Clients
{
    [Inject]
    public class BatchClient<TCreateResource, TUpdateResource, TResource>
        : ClientSender<TResource>,
          IBatchClient<TCreateResource, TUpdateResource, TResource>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
    {

        public Task<ActionResult<IEnumerable<TResource>>> BatchCreate(TCreateResource[] resources)
        {
            return SentPostMany("batch", resources);
        }

        public Task<ActionResult<IEnumerable<TResource>>> BatchUpdate(TUpdateResource[] resources)
        {
            return SentPutMany("batch", resources);
        }

        public Task<ActionResult<IEnumerable<string>>> BatchDelete(string[] ids)
        {
            return SendDeleteMany("batch", ids);
        }
    }
}