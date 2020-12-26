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
    public class BatchController<TCreateResource, TUpdateResource, TResource>
        : BaseController<IBatchApplication<TCreateResource, TUpdateResource, TResource>, TResource>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
    {
        [HttpPost("batch")]
        public virtual async Task<ActionResult<IEnumerable<TResource>>> BatchCreate(TCreateResource[] resources)
        {
            if (await IsUnauthorized(Operations.Create)) return Unauthorized();
            return await Application.BatchCreate(resources);
        }

        [HttpPut("batch")]
        public virtual async Task<ActionResult<IEnumerable<TResource>>> BatchUpdate(TUpdateResource[] resources)
        {
            if (await IsUnauthorized(Operations.Alter)) return Unauthorized();
            return await Application.BatchUpdate(resources);
        }

        [HttpDelete("batch")]
        public virtual async Task<ActionResult<IEnumerable<string>>> BatchDelete(string[] ids)
        {
            if (await IsUnauthorized(Operations.Delete)) return Unauthorized();
            return await Application.BatchDelete(ids);
        }
    }
}
