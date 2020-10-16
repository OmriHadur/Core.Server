using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using System.Collections.Generic;

namespace Core.Server.Tests.ResourceCreators.Interfaces
{
    public interface IResourceQuery<TResource> 
        where TResource : Resource
    {
        ActionResult<TResource> Get(string id);
        IEnumerable<TResource> Get(string[] ids);
        IEnumerable<TResource> Get();
    }
}
