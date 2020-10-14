using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Server.Common.Applications;
using Unity;
using Core.Server.Shared.Resources;
using Core.Server.Common.Attributes;

namespace Core.Server.Web.Controllers
{
    [InjectBoundleController]
    public class BatchController<TCreateResource, TUpdateResource, TResource>
        : BaseController<IBatchApplication<TCreateResource, TUpdateResource, TResource>>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
    {
        [HttpGet("batch")]
        public virtual async Task<ActionResult<IEnumerable<TResource>>> BatchGet(string[] ids)
        {
            return await Application.BatchGet(ids);
        }

        [HttpPost("batch")]
        public virtual async Task<ActionResult<IEnumerable<TResource>>> BatchCreate(TCreateResource[] resources)
        {
            return await Application.BatchCreate(resources);
        }

        [HttpPut("batch")]
        public virtual async Task<ActionResult<IEnumerable<TResource>>> BatchUpdate(TUpdateResource[] resources)
        {
            return await Application.BatchUpdate(resources);
        }

        [HttpDelete("batch")]
        public virtual async Task<ActionResult<IEnumerable<string>>> BatchDelete(string[] ids)
        {
            return await Application.BatchDelete(ids);
        }
    }
}
