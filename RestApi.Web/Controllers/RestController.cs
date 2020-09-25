using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestApi.Common.Applications;
using Unity;
using Microsoft.Extensions.Logging;
using RestApi.Shared.Resources;
using RestApi.Shared.Query;

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
            SetUser();
            return await Application.Get();
        }

        [HttpGet("{id}", Order=2)]
        public virtual async Task<ActionResult<TResource>> Get(string id)
        {
            SetUser();
            return await Application.Get(id);
        }

        [HttpPost("query")]
        public virtual async Task<ActionResult<IEnumerable<TResource>>> Query(QueryResource query)
        {
            SetUser();
            return await Application.Query(query);
        }

        [HttpPost]
        public virtual async Task<ActionResult<TResource>> Create(TCreateResource resource)
        {
            SetUser();
            return await Application.Create(resource);
        }

        [HttpPut("{id}", Order = 2)]
        public virtual async Task<ActionResult<TResource>> Update(string id, TCreateResource resource)
        {
            SetUser();
            return await Application.Update(id, resource);
        }

        [HttpDelete("{id}", Order = 2)]
        public virtual async Task<ActionResult<TResource>> Delete(string id)
        {
            SetUser();
            return await Application.Delete(id);
        }

        [HttpHead("{id}", Order = 2)]
        public virtual async Task<ActionResult<TResource>> Exists(string id)
        {
            SetUser();
            return await Application.Exists(id);
        }

        private void SetUser()
        {
            Application.CurrentUser = GetUser();
        }
    }
}
