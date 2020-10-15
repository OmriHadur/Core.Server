using Core.Server.Shared.Resources;

namespace Core.Server.Tests.ResourceCreation.Interfaces
{
    public interface IRandomResourceCreator<TCreateResource, TUpdateResource>
        where TCreateResource : CreateResource
        where TUpdateResource: UpdateResource
    {
        TCreateResource GetRandomCreateResource();
        TUpdateResource GetRandomUpdateResource();
    }
}
