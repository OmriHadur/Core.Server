using Core.Server.Shared.Resources;

namespace Core.Server.Tests.ResourceCreation.Interfaces
{
    public interface IRandomResourceCreator<TCreateResource, TUpdateResource, TResource>
        where TCreateResource : CreateResource
        where TUpdateResource: UpdateResource
        where TResource: Resource
    {
        TCreateResource GetRandomCreateResource();
        TCreateResource GetRandomCreateResource(TResource existingResource);
        TUpdateResource GetRandomUpdateResource(TResource existingResource);
    }
}
