using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using System.Threading.Tasks;

namespace Core.Server.Client.Interfaces
{
    public interface IAlterClient<TCreateResource, TUpdateResource, TResource>
        : IClientBase
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
    {
        Task<ActionResult<TResource>> Create(TCreateResource resource);

        Task<ActionResult<TResource>> CreateOrUpdate(string id, TCreateResource resource);

        Task<ActionResult<TResource>> Delete(string id);

        Task<ActionResult<TResource>> Update(string id, TUpdateResource resource);
    }
}