using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using System.Collections.Generic;

namespace Core.Server.Tests.ResourceCreators.Interfaces
{
    public interface IResourceBatcher<TCreateResource, TUpdateResource,TResource>
        where TCreateResource : CreateResource, new()
        where TUpdateResource: UpdateResource
        where TResource : Resource
    {
        ActionResult<IEnumerable<TResource>> Create(int amount);
        ActionResult<IEnumerable<TResource>> Create(TCreateResource[] createResources);
    }
}
