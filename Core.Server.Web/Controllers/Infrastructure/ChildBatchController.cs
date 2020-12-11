using Core.Server.Common.Applications;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Server.Web.Controllers
{
    [InjectChildBoundleController]
    public class ChildBatchController<TCreateResource, TUpdateResource, TParentResource>
        : BaseController<IChildBatchApplication<TCreateResource, TUpdateResource, TParentResource>>
        where TCreateResource : ChildCreateResource
        where TUpdateResource : ChildUpdateResource
        where TParentResource : Resource
    {
        [HttpPost("batch")]
        public virtual async Task<ActionResult<IEnumerable<TParentResource>>> BatchCreate(TCreateResource[] resources)
        {
            return await Application.BatchCreate(resources);
        }

        [HttpPut("batch")]
        public virtual async Task<ActionResult<IEnumerable<TParentResource>>> BatchUpdate(TUpdateResource[] resources)
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
