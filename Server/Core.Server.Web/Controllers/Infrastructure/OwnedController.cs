using Core.Server.Common.Applications;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using Core.Server.Web.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Server.Web.Controllers
{
    [InjectBoundleController]
    [Authorize]
    public class OwnedController<TResource>
        : BaseController<IOwnedApplication<TResource>, TResource>
        where TResource : Resource
    {
        [HttpGet("owned")]
        public virtual async Task<ActionResult<IEnumerable<TResource>>> GetAll()
        {
            return await Application.GetAllOwned();
        }

        [HttpHead("owned")]
        public virtual async Task<ActionResult> Any()
        {
            return await Application.Any();
        }

        [HttpPost("assign")]
        public virtual async Task<ActionResult> Assign(string resourceId)
        {
            return await Application.Assign(resourceId);
        }

        [HttpPost("reassign")]
        public virtual async Task<ActionResult> Reassign(ReassginResource reassginResource)
        {
            if (await IsUnauthorized(Operations.Reassign)) return Unauthorized();
            return await Application.Reassign(reassginResource);
        }
    }
}
