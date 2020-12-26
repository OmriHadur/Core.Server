using Core.Server.Common.Applications;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using Core.Server.Web.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Server.Web.Controllers
{
    [InjectBoundleController]
    public class LookupController<TResource>
        : BaseController<ILookupApplication<TResource>, TResource>
        where TResource : Resource
    {
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TResource>>> GetAll()
        {
            if (await IsUnauthorized(Operations.Read)) return Unauthorized();
            return await Application.GetAll();
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TResource>> GetById(string id)
        {
            if (await IsUnauthorized(Operations.Read)) return Unauthorized();
            return await Application.GetById(id);
        }

        [HttpGet("inline")]
        public virtual async Task<ActionResult<IEnumerable<TResource>>> GetByIds([FromQuery] string ids)
        {
            if (await IsUnauthorized(Operations.Read)) return Unauthorized();
            return await Application.GetByIds(ids.Split(','));
        }

        [HttpPost("ids")]
        public virtual async Task<ActionResult<IEnumerable<TResource>>> GetByIds(string[] ids)
        {
            if (await IsUnauthorized(Operations.Read)) return Unauthorized();
            return await Application.GetByIds(ids);
        }

        [HttpHead("{id}")]
        public virtual async Task<ActionResult> Exists(string id)
        {
            return await Application.Exists(id);
        }

        [HttpHead()]
        public virtual async Task<ActionResult> Any()
        {
            return await Application.Any();
        }
    }
}
