using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Server.Common.Applications
{
    public interface IOwnedApplication<TResource> :
        IBaseApplication
        where TResource : Resource
    {
        Task<ActionResult<IEnumerable<TResource>>> GetAllOwned();

        Task<ActionResult> Any();

        Task<ActionResult> Assign(string resourceId);

        Task<ActionResult> Reassign(ReassginResource reassginResource);
    }
}