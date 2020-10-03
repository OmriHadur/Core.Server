using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Server.Common.Applications;
using Unity;
using Microsoft.Extensions.Logging;
using Core.Server.Shared.Resources;
using Core.Server.Shared.Query;

namespace Core.Server.Web.Controllers
{
    public class RestController<TCreateResource, TUpdateResource, TResource>
        : QueryController<TResource>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
    {
        [Dependency]
        public IRestApplication<TCreateResource, TUpdateResource, TResource> RestApplication { get; set; }

        [HttpPost]
        public virtual async Task<ActionResult<TResource>> Create(TCreateResource resource)
        {
            return await RestApplication.Create(resource);
        }

        [HttpPut]
        public virtual async Task<ActionResult<TResource>> Update(TUpdateResource resource)
        {
            return await RestApplication.Update(resource);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<TResource>> Delete(string id)
        {
            return await RestApplication.Delete(id);
        }
    }
}
