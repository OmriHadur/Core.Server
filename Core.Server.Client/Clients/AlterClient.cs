using System.Threading.Tasks;
using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Injection.Attributes;
using Core.Server.Shared.Resources;

namespace Core.Server.Client.Clients
{
    [Inject]
    public class AlterClient<TCreateResource, TUpdateResource, TResource>
        : ClientSender<TResource>,
          IAlterClient<TCreateResource, TUpdateResource, TResource>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
    {
        public Task<ActionResult<TResource>> Create(TCreateResource resource)
        {
            return SentPost(resource);
        }

        public Task<ActionResult<TResource>> Replace(string id, TCreateResource resource)
        {
            return SentPut(id,resource);
        }

        public Task<ActionResult<TResource>> Delete(string id)
        {
            return SendDelete(id);
        }

        public Task<ActionResult<TResource>> Update(string id, TUpdateResource resource)
        {
            return SendPatch(id, resource);
        }
    }
}