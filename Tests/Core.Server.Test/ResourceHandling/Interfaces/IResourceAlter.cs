using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using System;

namespace Core.Server.Test.ResourceCreators.Interfaces
{
    public interface IResourceAlter<TAlterResource, TResource>
        where TResource : Resource
    {
        ActionResult<TResource> Create(Action<TAlterResource> editFunc = null);
        ActionResult<TResource> Create(TAlterResource createResource);
        ActionResult<TResource> Replace(Action<TAlterResource> editFunc = null);
        ActionResult<TResource> Update(Action<TAlterResource> editFunc = null);
    }
}
