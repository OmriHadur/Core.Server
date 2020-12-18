using Core.Server.Common.Applications;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using Core.Server.Web.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core.Server.Web.Controllers
{
    [InjectBoundleController]
    public class AlterController<TCreateResource, TUpdateResource, TResource>
        : BaseController<IAlterApplication<TCreateResource, TUpdateResource, TResource>, TResource>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
    {
        [HttpPost]
        public virtual async Task<ActionResult<TResource>> Create(TCreateResource resource)
        {
            if (await IsUnauthorized(Operations.Create)) return Unauthorized();
            return await Application.Create(resource);
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult<TResource>> Replace(string id, TCreateResource resource)
        {
            if (await IsUnauthorized(Operations.Alter)) return Unauthorized();
            return await Application.Replace(id, resource);
        }

        [HttpPatch("{id}")]
        public virtual async Task<ActionResult<TResource>> UpdateFromBody(string id, TUpdateResource resource)
        {
            if (await IsUnauthorized(Operations.Alter)) return Unauthorized();
            return await Application.Update(id, resource);
        }

        [HttpPatch("{id}/inline")]
        public virtual async Task<ActionResult<TResource>> UpdateFromQuery(string id, [FromQuery] TUpdateResource resource)
        {
            if (await IsUnauthorized(Operations.Alter)) return Unauthorized();
            return await Application.Update(id, resource);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete(string id)
        {
            if (await IsUnauthorized(Operations.Delete)) return Unauthorized();
            return await Application.Delete(id);
        }

        [HttpDelete()]
        public virtual async Task<ActionResult> DeleteAll()
        {
            if (await IsUnauthorized(Operations.Delete)) return Unauthorized();
            return await Application.DeleteAll();
        }
    }
}
