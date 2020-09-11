using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestApi.Common.Applications;
using Unity;
using Microsoft.Extensions.Logging;
using RestApi.Standard.Shared.Resources;

namespace RestApi.Web.Controllers
{
    public class RestController<TCreateResource, TResource>
        : Controller
        where TCreateResource : CreateResource
        where TResource : Resource
    {
        [Dependency]
        public IRestApplication<TCreateResource, TResource> Application { get; set; }

        [Dependency]
        public ILogger<TResource> Logger;

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TResource>>> Get()
        {
            Application.CurrentUser = GetUser();
            return await Application.Get();
        }

        [HttpGet("{id}", Order=2)]
        public virtual async Task<ActionResult<TResource>> Get(string id)
        {
            Application.CurrentUser = GetUser();
            return await Application.Get(id);
        }

        [HttpPost]
        public virtual async Task<ActionResult<TResource>> Create(TCreateResource resource)
        {
            Application.CurrentUser = GetUser();
            return await Application.Create(resource);
        }

        [HttpPut("{id}", Order = 2)]
        public virtual async Task<ActionResult<TResource>> Update(string id, TCreateResource resource)
        {
            Application.CurrentUser = GetUser();
            return await Application.Update(id, resource);
        }

        [HttpDelete("{id}", Order = 2)]
        public virtual async Task<ActionResult<TResource>> Delete(string id)
        {
            Application.CurrentUser = GetUser();
            return await Application.Delete(id);
        }

        [HttpHead("{id}", Order = 2)]
        public virtual async Task<ActionResult<TResource>> Exists(string id)
        {
            Application.CurrentUser = GetUser();
            return await Application.Exists(id);
        }
    }
}
