using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using System;

namespace Core.Server.Test.ResourceCreators.Interfaces
{
    public interface IChildResourceAlter<TChildAlterResource, TParentResource, TChildResource>
        : IResourceAlter<TChildAlterResource, TParentResource>
        where TChildAlterResource : ChildAlterResource
        where TParentResource : Resource
        where TChildResource : Resource
    {
        TChildResource[] GetChildResource(TParentResource parentResource);

        ActionResult<TParentResource> DeleteLastChild();
    }
}