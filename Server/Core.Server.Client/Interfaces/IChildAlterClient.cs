using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using System.Threading.Tasks;

namespace Core.Server.Client.Interfaces
{
    public interface IChildAlterClient<TChildAlterResource, TResource>
        : IClientBase
        where TChildAlterResource : ChildAlterResource
        where TResource : Resource
    {
        Task<ActionResult<TResource>> Create(TChildAlterResource resource);

        Task<ActionResult<TResource>> Replace(string id, TChildAlterResource resource);

        Task<ActionResult<TResource>> Update(string id, TChildAlterResource resource);

        Task<ActionResult<TResource>> Delete(string id, ChildAlterResource childResource);

        Task<ActionResult<TResource>> DeleteAll(ChildAlterResource childResource);
    }
}