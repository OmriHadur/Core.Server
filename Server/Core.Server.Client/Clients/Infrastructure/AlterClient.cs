using System.Threading.Tasks;
using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;

namespace Core.Server.Client.Clients
{
    [Inject]
    public class AlterClient<TAlterResource, TResource>
        : ClientSender<TResource>,
          IAlterClient<TAlterResource, TResource>
        where TResource : Resource
    {
        public Task<ActionResult<TResource>> Create(TAlterResource resource)
        {
            return SentPost(resource);
        }

        public Task<ActionResult<TResource>> Replace(string id, TAlterResource resource)
        {
            return SentPut(id,resource);
        }

        public Task<ActionResult> Delete(string id)
        {
            return SendDelete(id);
        }

        public Task<ActionResult<TResource>> Update(string id, TAlterResource resource)
        {
            return SendPatch(id, resource);
        }
    }
}