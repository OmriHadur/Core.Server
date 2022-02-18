using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using System.Collections.Generic;

namespace Core.Server.Test.ResourceCreators.Interfaces
{
    public interface IResourceBatch<TAlterResource, TResource>
        where TResource : Resource
    {
        ActionResult<IEnumerable<TResource>> Create(int amount);
        ActionResult<IEnumerable<TResource>> Create(IEnumerable<TAlterResource> createResources);
        ActionResult<IEnumerable<TResource>> Update(IEnumerable<TAlterResource> updateResources);
        ActionResult<IEnumerable<string>> Delete(IEnumerable<string> ids);
    }
}
