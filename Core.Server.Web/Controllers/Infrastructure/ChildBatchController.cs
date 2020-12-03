using Core.Server.Common.Applications;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Server.Web.Controllers
{
    [InjectBoundleController]
    public class ChildBatchController<TCreateResource, TUpdateResource, TResource>
        : BaseController<IChildBatchApplication<TCreateResource, TUpdateResource, TResource>>
        where TCreateResource : ChildCreateResource
        where TUpdateResource : ChildUpdateResource
        where TResource : Resource
    {
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
        public virtual async Task<ActionResult<IEnumerable<string>>> BatchDelete(ChildBatchDeleteResource childBatchDeleteResource)
        {
            return await Application.BatchDelete(childBatchDeleteResource);
        }
    }
}
