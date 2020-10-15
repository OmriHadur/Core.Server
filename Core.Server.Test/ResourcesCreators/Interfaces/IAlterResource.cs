using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using System;

namespace Core.Server.Tests.ResourceCreators.Interfaces
{
    public interface IAlterResource<TCreateResource, TUpdateResource, TResource>
        : ICreateResource<TResource>
        , IResourceDeleter
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
    {
        ActionResult<TResource> Create(Func<TCreateResource> editFunc);
        ActionResult<TResource> Update();
        ActionResult<TResource> Update(Func<TUpdateResource> editFunc);
    }
}
