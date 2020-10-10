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
    public class AlterController<TCreateResource, TUpdateResource, TResource>
        : QueryController<TResource>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
    {
        [Dependency]
        public IAlterApplication<TCreateResource, TUpdateResource, TResource> AlterApplication;

        [HttpPost]
        public virtual async Task<ActionResult<TResource>> Create(TCreateResource resource)
        {
            return await AlterApplication.Create(resource);
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult<TResource>> CreateOrUpdate(string id,TCreateResource resource)
        {
            return await AlterApplication.CreateOrUpdate(id,resource);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<TResource>> Delete(string id)
        {
            return await AlterApplication.Delete(id);
        }
    }
}
