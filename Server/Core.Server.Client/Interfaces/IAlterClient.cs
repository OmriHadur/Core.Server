using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using System.Threading.Tasks;

namespace Core.Server.Client.Interfaces
{
    public interface IAlterClient<TAlterResource, TResource>
        : IClientBase
        where TResource : Resource
    {
        Task<ActionResult<TResource>> Create(TAlterResource resource);

        Task<ActionResult<TResource>> Replace(string id, TAlterResource resource);

        Task<ActionResult> Delete(string id);

        Task<ActionResult<TResource>> Update(string id, TAlterResource resource);
    }
}