using Core.Server.Client.Results;
using Core.Server.Shared.Resources;

namespace Core.Server.Tests.ResourceCreators.Interfaces
{
    public interface IResourceCreate<TResource> 
        : IResourceDelete
        where TResource : Resource
    {
        ActionResult<TResource> Create();
        TResource GetOrCreate();
        TResource GetIfExist();
    }
}
