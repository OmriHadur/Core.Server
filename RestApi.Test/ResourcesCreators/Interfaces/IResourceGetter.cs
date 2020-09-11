using RestApi.Client.Results;
using RestApi.Shared.Resources;

namespace RestApi.Tests.RestResourcesCreators.Interfaces
{
    public interface IResourceGetter<TResource> : IResourceDeleter
        where TResource : Resource
    {
        ActionResult<TResource> Create();

        ActionResult<TResource> Get(string id);
    }
}
