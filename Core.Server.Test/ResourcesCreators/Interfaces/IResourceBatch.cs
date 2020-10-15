using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using System.Collections.Generic;

namespace Core.Server.Tests.ResourceCreators.Interfaces
{
    public interface IResourceBatch<TCreateResource, TUpdateResource,TResource>
        where TCreateResource : CreateResource
        where TUpdateResource: UpdateResource
        where TResource : Resource
    {
        ActionResult<IEnumerable<TResource>> Create(int amount);
        ActionResult<IEnumerable<TResource>> Create(TCreateResource[] createResources);
    }
}
