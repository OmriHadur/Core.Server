using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using System.Threading.Tasks;

namespace Core.Server.Client.Interfaces
{
    public interface IChildAlterClient<TCreateResource, TUpdateResource, TResource>
        : IClientBase
        where TCreateResource : ChildCreateResource
        where TUpdateResource : ChildUpdateResource
        where TResource : Resource
    {
        Task<ActionResult<TResource>> Create(TCreateResource resource);

        Task<ActionResult<TResource>> Replace(string id, TCreateResource resource);

        Task<ActionResult<TResource>> Update(string id, TUpdateResource resource);

        Task<ActionResult<TResource>> Delete(string id, ChildDeleteResource childResource);

        Task<ActionResult<TResource>> DeleteAll(ChildDeleteResource childResource);
    }
}