using Core.Server.Client.Results;
using Core.Server.Shared.Resources;

namespace Core.Server.Test.ResourceCreators.Interfaces
{
    public interface IResourceCreate<TResource> 
        : IResourceDelete
        where TResource : Resource
    {
        ActionResult<TResource> Create();
        ActionResult Delete();

        TResource GetOrCreate();

        TResource GetIfExist();
    }
}
