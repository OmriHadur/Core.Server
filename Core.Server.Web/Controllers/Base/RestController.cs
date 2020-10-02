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
    [ApiController]
    [Route("api/[controller]")]
    public class RestController<TCreateResource, TUpdateResource, TResource>
        : Controller
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
    {
        [Dependency]
        public IRestApplication<TCreateResource, TUpdateResource, TResource> Application { get; set; }

        [Dependency]
        public ILogger<TResource> Logger;

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TResource>>> Get()
        {
            return await Application.Get();
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TResource>> Get(string id)
        {
            return await Application.Get(id);
        }

        [HttpPost("query")]
        public virtual async Task<ActionResult<IEnumerable<TResource>>> Query(QueryResource query)
        {
            return await Application.Query(query);
        }

        [HttpPost]
        public virtual async Task<ActionResult<TResource>> Create(TCreateResource resource)
        {
            return await Application.Create(resource);
        }

        [HttpPut]
        public virtual async Task<ActionResult<TResource>> Update(TUpdateResource resource)
        {
            return await Application.Update(resource);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<TResource>> Delete(string id)
        {
            return await Application.Delete(id);
        }

        [HttpHead("{id}")]
        public virtual async Task<ActionResult<TResource>> Exists(string id)
        {
            return await Application.Exists(id);
        }

        private void SetUser()
        {
            Application.CurrentUser = GetUser();
        }
    }
}
