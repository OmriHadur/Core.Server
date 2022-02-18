using Core.Server.Shared.Resources;

namespace Core.Server.Test.ResourceCreation.Interfaces
{
    public interface IRandomResourceCreator<TAlterResource, TResource>
        where TResource : Resource
    {
        TAlterResource GetRandomCreateResource();
        TAlterResource GetRandomReplacResource(TResource existingResource);
        TAlterResource GetRandomUpdateResource(TResource existingResource);
    }
}
