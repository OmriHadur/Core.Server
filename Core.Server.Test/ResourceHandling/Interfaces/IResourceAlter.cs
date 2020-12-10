using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using System;

namespace Core.Server.Tests.ResourceCreators.Interfaces
{
    public interface IResourceAlter<TCreateResource, TUpdateResource, TResource>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
    {
        ActionResult<TResource> Create(Action<TCreateResource> editFunc = null);
        ActionResult<TResource> Create(TCreateResource createResource);
        ActionResult<TResource> Replace(Action<TCreateResource> editFunc = null);
        ActionResult<TResource> Update(Action<TUpdateResource> editFunc = null);
    }
}
