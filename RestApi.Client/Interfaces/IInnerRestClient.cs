
using RestApi.Standard.Client.Results;
using RestApi.Standard.Shared.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApi.Standard.Client.Interfaces
{
    public interface IInnerRestClient<TCreateResource, TResource>:
        IClientBase
        where TCreateResource : CreateResource
        where TResource : Resource
    {
        Task<ActionResult<IEnumerable<TResource>>> Get(string parentId);

        Task<ActionResult<TResource>> Get(string parentId, string id);

        Task<ActionResult<TResource>> Create(string parentId, TCreateResource resource);

        Task<ActionResult<TResource>> Update(string parentId, string id, TCreateResource resource);

        Task<ActionResult<TResource>> Delete(string parentId, string id);
    }
}