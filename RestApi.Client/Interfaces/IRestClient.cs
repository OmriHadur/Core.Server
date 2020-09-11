
using RestApi.Standard.Client.Results;
using RestApi.Standard.Shared.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApi.Standard.Client.Interfaces
{
    public interface IRestClient<TCreateResource, TResource> :
        IClientBase
        where TCreateResource : CreateResource
        where TResource : Resource
    {
        Task<ActionResult<IEnumerable<TResource>>> Get();

        Task<ActionResult<TResource>> Get(string id);

        Task<ActionResult<TResource>> Create(TCreateResource resource);

        Task<ActionResult<TResource>> Update(string id, TCreateResource resource);

        Task<ActionResult<TResource>> Delete(string id);
    }
}