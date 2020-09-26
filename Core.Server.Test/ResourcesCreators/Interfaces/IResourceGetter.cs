using Core.Server.Client.Results;
using Core.Server.Shared.Resources;

namespace Core.Server.Tests.ResourceCreators.Interfaces
{
    public interface IResourceGetter<TResource> : IResourceDeleter
        where TResource : Resource
    {
        ActionResult<TResource> Create();

        ActionResult<TResource> Get(string id);
    }
}
