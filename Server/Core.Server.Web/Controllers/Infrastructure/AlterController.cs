using Core.Server.Common.Applications;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using Core.Server.Web.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace Core.Server.Web.Controllers
{
    [InjectBoundleController]
    public class AlterController<TAlterResource, TResource>
        : BaseController<IAlterApplication<TAlterResource, TResource>, TResource>
        where TResource : Resource
    {
        [HttpPost]
        public virtual async Task<ActionResult<TResource>> Create(TAlterResource resource)
        {
            //var m = new ModelStateDictionary();
            //m.AddModelError("value", "is null");
            //var b= ValidationProblem(m);
            //return b;
            if (await IsUnauthorized(Operations.Create)) return Unauthorized();
            return await Application.Create(resource);
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult<TResource>> Replace(string id, TAlterResource resource)
        {
            if (await IsUnauthorized(Operations.Alter)) return Unauthorized();
            return await Application.Replace(id, resource);
        }

        [HttpPatch("{id}")]
        public virtual async Task<ActionResult<TResource>> UpdateFromBody(string id, TAlterResource resource)
        {
            if (await IsUnauthorized(Operations.Alter)) return Unauthorized();
            return await Application.Update(id, resource);
        }

        [HttpPatch("{id}/inline")]
        public virtual async Task<ActionResult<TResource>> UpdateFromQuery(string id, [FromQuery] TAlterResource resource)
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
