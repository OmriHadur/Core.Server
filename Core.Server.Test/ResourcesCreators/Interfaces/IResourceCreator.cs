using Core.Server.Client.Results;
using Core.Server.Shared.Resources;

namespace Core.Server.Tests.ResourceCreators.Interfaces
{
    public interface IResourceCreator<TCreateResource,TResource> : 
        IResourceGetter<TResource>
        where TCreateResource : CreateResource, new()
        where TResource : Resource
    {
        ActionResult<TResource> Create(TCreateResource createResource);
        ActionResult<TResource> Update(string id, TCreateResource resourceToCreate);
        TCreateResource GetRandomCreateResource();
    }
}
