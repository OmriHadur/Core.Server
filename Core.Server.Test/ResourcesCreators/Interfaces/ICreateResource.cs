using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using System;

namespace Core.Server.Tests.ResourceCreators.Interfaces
{
    public interface ICreateResource<TResource> 
        where TResource : Resource
    {
        ActionResult<TResource> Create();
        TResource GetOrCreate();
    }
}
