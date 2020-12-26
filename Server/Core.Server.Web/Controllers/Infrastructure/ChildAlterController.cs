using Core.Server.Common.Applications;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using Core.Server.Web.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core.Server.Web.Controllers
{
    [InjectChildBoundleController]
    public class ChildAlterController<TCreateResource, TUpdateResource, TParentResource>
        : BaseController<IChildAlterApplication<TCreateResource, TUpdateResource, TParentResource>, TParentResource>
        where TCreateResource : ChildCreateResource
        where TUpdateResource : ChildUpdateResource
        where TParentResource : Resource
    {
        [HttpPost]
        public virtual async Task<ActionResult<TParentResource>> Create(TCreateResource resource)
        {
            if (await IsUnauthorized(Operations.Alter)) return Unauthorized();
            return await Application.Create(resource);
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult<TParentResource>> Replace(string id, TCreateResource resource)
        {
            if (await IsUnauthorized(Operations.Alter)) return Unauthorized();
            return await Application.Replace(id, resource);
        }

        [HttpPatch("{id}")]
        public virtual async Task<ActionResult<TParentResource>> UpdateFromBody(string id, TUpdateResource resource)
        {
            if (await IsUnauthorized(Operations.Alter)) return Unauthorized();
            return await Application.Update(id, resource);
        }

        [HttpPatch("{id}/inline")]
        public virtual async Task<ActionResult<TParentResource>> UpdateFromQuery(string id, [FromQuery] TUpdateResource resource)
        {
            if (await IsUnauthorized(Operations.Alter)) return Unauthorized();
            return await Application.Update(id, resource);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<TParentResource>> Delete(string id, ChildDeleteResource childResource)
        {
            if (await IsUnauthorized(Operations.Alter)) return Unauthorized();
            return await Application.Delete(childResource.ParentId, id);
        }

        [HttpDelete()]
        public virtual async Task<ActionResult<TParentResource>> DeleteAll(ChildDeleteResource childResource)
        {
            if (await IsUnauthorized(Operations.Alter)) return Unauthorized();
            return await Application.DeleteAll(childResource.ParentId);
        }
    }
}