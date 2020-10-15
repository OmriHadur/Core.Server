using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using System.Collections.Generic;

namespace Core.Server.Tests.ResourceCreators.Interfaces
{
    public interface IQueryResource<TResource> 
        where TResource : Resource
    {
        ActionResult<TResource> Get(string id);
        IEnumerable<TResource> GetAll();
    }
}
