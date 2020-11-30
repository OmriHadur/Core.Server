using Core.Server.Common.Applications;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core.Server.Web.Controllers
{
    [InjectChildBoundleController]
    public class AlterChildController<TCreateResource, TUpdateResource, TResource>
        : BaseController<IChildAlterApplication<TCreateResource, TUpdateResource, TResource>>
        where TCreateResource : ChildCreateResource
        where TUpdateResource : ChildUpdateResource
        where TResource : Resource
    {
        [HttpPost]
        public virtual async Task<ActionResult<TResource>> Create(TCreateResource resource)
        {
            return await Application.Create(resource);
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult<TResource>> Replace(string id, TCreateResource resource)
        {
            return await Application.Replace(id, resource);
        }

        [HttpPatch("{id}")]
        public virtual async Task<ActionResult<TResource>> UpdateFromBody(string id, TUpdateResource resource)
        {
            return await Application.Update(id, resource);
        }

        [HttpPatch("{id}/inline")]
        public virtual async Task<ActionResult<TResource>> UpdateFromQuery(string id, [FromQuery] TUpdateResource resource)
        {
            return await Application.Update(id, resource);
        }

        [HttpDelete("{parentId}/{id}")]
        public virtual async Task<ActionResult<TResource>> Delete(string parentId, string id)
        {
            return await Application.Delete(parentId, id);
        }

        [HttpDelete("{parentId}")]
        public virtual async Task<ActionResult<TResource>> DeleteAll(string parentId)
        {
            return await Application.DeleteAll(parentId);
        }
    }
}