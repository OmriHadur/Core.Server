using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Server.Shared.Resources;

namespace Core.Server.Common.Applications
{
    public interface IOwnedApplication<TResource> :
        IBaseApplication
        where TResource : Resource
    {
        Task<ActionResult<IEnumerable<TResource>>> GetAllOwned();

        Task<ActionResult> Any();

        Task<ActionResult> ReAssign(string resourceId,string userId);
    }
}