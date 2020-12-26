using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using System.Collections.Generic;

namespace Core.Server.Test.ResourceCreators.Interfaces
{
    public interface IResourceOwned<TResource>
        where TResource : Resource
    {
        ActionResult<IEnumerable<TResource>> GetAll();

        ActionResult Any();

        ActionResult Reassigen(string resourceId);
    }
}
