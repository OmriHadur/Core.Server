using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using System;

namespace Core.Server.Tests.ResourceCreators.Interfaces
{
    public interface IChildResourceAlter<TCreateResource, TUpdateResource, TParentResource>
        where TCreateResource : ChildCreateResource
        where TUpdateResource : ChildUpdateResource
        where TParentResource : Resource
    {
        ActionResult<TParentResource> Create(Action<TCreateResource> editFunc = null);
        ActionResult<TParentResource> Create(TCreateResource createResource);
        ActionResult<TParentResource> Replace(string childId, Action<TCreateResource> editFunc = null);
        ActionResult<TParentResource> Update(string childId, Action<TUpdateResource> editFunc = null);
    }
}
