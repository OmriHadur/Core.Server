
using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Server.Client.Interfaces
{
    public interface IInnerRestClient<TCreateResource, TUpdateResource, TResource> :
        IClientBase
        where TCreateResource : CreateResource
        where TUpdateResource: UpdateResource
        where TResource : Resource
    {
        Task<ActionResult<IEnumerable<TResource>>> Get(string parentId);

        Task<ActionResult<TResource>> Get(string parentId, string id);

        Task<ActionResult<TResource>> Create(string parentId, TCreateResource resource);

        Task<ActionResult<TResource>> Update(string parentId, string id, TUpdateResource resource);

        Task<ActionResult<TResource>> Delete(string parentId, string id);
    }
}