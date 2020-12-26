using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using System.Collections.Generic;

namespace Core.Server.Test.ResourceCreators.Interfaces
{
    public interface IChildResourceBatch<TChildAlterResource, TParentResource, TChildResource>
        where TChildAlterResource : ChildAlterResource
        where TParentResource : Resource
        where TChildResource : Resource
    {
        ActionResult<IEnumerable<TParentResource>> Create(int amount);

        TChildResource[] GetChildResource(TParentResource parentResource);
    }
}