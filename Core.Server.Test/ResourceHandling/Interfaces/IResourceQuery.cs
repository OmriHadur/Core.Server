using Core.Server.Client.Results;
using Core.Server.Shared.Query;
using Core.Server.Shared.Resources;
using System.Collections.Generic;

namespace Core.Server.Test.ResourceCreators.Interfaces
{
    public interface IResourceQuery<TResource>
        where TResource : Resource
    {
        ActionResult<IEnumerable<TResource>> Query(QueryResource queryResource);
    }
}
