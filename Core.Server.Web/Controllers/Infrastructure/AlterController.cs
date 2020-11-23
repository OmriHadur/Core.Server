﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Server.Common.Applications;
using Core.Server.Shared.Resources;
using Core.Server.Common.Attributes;

namespace Core.Server.Web.Controllers
{
    [InjectBoundleController]
    public class AlterController<TCreateResource, TUpdateResource, TResource>
        : BaseController<IAlterApplication<TCreateResource, TUpdateResource, TResource>>
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

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete(string id)
        {
            return await Application.Delete(id);
        }

        [HttpDelete()]
        public virtual async Task<ActionResult> DeleteAll()
        {
            return await Application.DeleteAll();
        }
    }
}