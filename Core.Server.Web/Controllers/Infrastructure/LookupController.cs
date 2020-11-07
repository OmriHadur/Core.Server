using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Server.Common.Applications;
using Core.Server.Shared.Resources;
using Core.Server.Injection.Attributes;

namespace Core.Server.Web.Controllers
{
    [InjectBoundleController]
    public class LookupController<TResource>
        : BaseController<ILookupApplication<TResource>>
        where TResource : Resource
    {
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TResource>>> GetAll()
        {
            return await Application.GetAll();
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TResource>> GetById(string id)
        {
            return await Application.GetById(id);
        }

        [HttpGet("inline")]
        public virtual async Task<ActionResult<IEnumerable<TResource>>> GetByIds([FromQuery] string ids)
        {
            return await Application.GetByIds(ids.Split(','));
        }

        [HttpPost("ids")]
        public virtual async Task<ActionResult<IEnumerable<TResource>>> GetByIds(string[] ids)
        {
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
