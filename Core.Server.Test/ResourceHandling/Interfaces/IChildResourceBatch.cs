using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using System;
using System.Collections.Generic;

namespace Core.Server.Test.ResourceCreators.Interfaces
{
    public interface IChildResourceBatch<TCreateResource, TUpdateResource, TParentResource, TChildResource>
        where TCreateResource : ChildCreateResource
        where TUpdateResource : ChildUpdateResource
        where TParentResource : Resource
        where TChildResource : Resource
    {
        ActionResult<IEnumerable<TParentResource>> Create(int amount);

        TChildResource[] GetChildResource(TParentResource parentResource);
    }
}