using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Server.Common.Applications;
using Core.Server.Shared.Resources;
using Core.Server.Common.Attributes;
using System.Collections.Generic;

namespace Core.Server.Web.Controllers
{
    [InjectBoundleController]
    public class OwnedController<TResource>
        : BaseController<IOwnedApplication<TResource>>
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
    }
}
