using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using System;

namespace Core.Server.Tests.ResourceCreators.Interfaces
{
    public interface IChildResourceAlter<TCreateResource, TUpdateResource, TParentResource, TChildResource>
        : IResourceAlter<TCreateResource, TUpdateResource, TParentResource>
        where TCreateResource : ChildCreateResource
        where TUpdateResource : ChildUpdateResource
        where TParentResource : Resource
        where TChildResource : Resource
    {
        TChildResource[] GetChildResource(TParentResource parentResource);

        ActionResult<TParentResource> DeleteLastChild();
    }
}