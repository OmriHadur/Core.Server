using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestApi.Common.Applications;
using RestApi.Standard.Shared.Resources;
using Unity;

namespace RestApi.Web.Controllers
{
    public class InnerRestController<TCreateResource, TResource>
        : Controller
        where TCreateResource : CreateResource
        where TResource : Resource
    {
        [Dependency]
        public IInnerRestApplication<TCreateResource, TResource> Application { get; set; }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TResource>>> Get(string parentId)
        {
            Application.CurrentUser = GetUser();
            return await Application.Get(parentId);
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TResource>> Get(string parentId, string id)
        {
            Application.CurrentUser = GetUser();
            return await Application.Get(parentId, id);
        }

        [HttpPost]
        public virtual async Task<ActionResult<TResource>> Create(string parentId, TCreateResource resource)
        {
            Application.CurrentUser = GetUser();
            return await Application.Create(parentId, resource);
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult<TResource>> Update(string parentId, string id, TCreateResource resource)
        {
            Application.CurrentUser = GetUser();
            return await Application.Update(parentId, id, resource);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<TResource>> Delete(string parentId, string id)
        {
            Application.CurrentUser = GetUser();
            return await Application.Delete(parentId, id);
        }
    }
}
