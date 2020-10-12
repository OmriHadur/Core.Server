using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Server.Common.Applications;
using Core.Server.Shared.Resources;

namespace Core.Server.Web.Controllers
{
    public class AlterController<TApplication, TCreateResource, TUpdateResource, TResource>
        : QueryController<TApplication,TResource>
        where TApplication : IAlterApplication<TCreateResource, TUpdateResource, TResource>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
    {
        [HttpPost]
        public virtual async Task<ActionResult<TResource>> Create(TCreateResource resource)
        {
            return await Application.Create(resource);
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult<TResource>> CreateOrUpdate(string id,TCreateResource resource)
        {
            return await Application.CreateOrUpdate(id,resource);
        }

        [HttpPatch("{id}")]
        public virtual async Task<ActionResult<TResource>> Update(string id, TUpdateResource resource)
        {
            return await Application.Update(id, resource);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<TResource>> Delete(string id)
        {
            return await Application.Delete(id);
        }
    }
}
