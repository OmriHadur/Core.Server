using Core.Server.Client.Results;
using Core.Server.Shared.Resources;

namespace Core.Server.Tests.ResourceCreators.Interfaces
{
    public interface IResourceCreator<TCreateResource, TUpdateResource,TResource> : 
        IResourceGetter<TResource>
        where TCreateResource : CreateResource, new()
        where TUpdateResource: UpdateResource
        where TResource : Resource
    {
        ActionResult<TResource> Create(TCreateResource createResource);
        ActionResult<TResource> Update(string id, TUpdateResource updateResource);
        TCreateResource GetRandomCreateResource();
        TUpdateResource GetRandomUpdateResource();
    }
}
