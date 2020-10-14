using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Shared.Resources;

namespace Core.Server.Client.Clients
{
    public abstract class BatchClient<TCreateResource, TUpdateResource, TResource>
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

        public Task<ActionResult<IEnumerable<string>>> BatchDelete(string[] ids)
        {
            return SendDeleteMany("batch", ids);
        }

        public Task<ActionResult<IEnumerable<TResource>>> BatchGet(string[] ids)
        {
            return SendGetMany("batch");
        }

        public Task<ActionResult<IEnumerable<TResource>>> BatchUpdate(TUpdateResource[] resources)
        {
            return SentPutMany("batch", resources);
        }
    }
}