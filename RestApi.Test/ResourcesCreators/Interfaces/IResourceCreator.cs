using RestApi.Client.Results;
using RestApi.Shared.Resources;

namespace RestApi.Tests.ResourceCreators.Interfaces
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
