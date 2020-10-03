using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Server.Common.Applications;
using Core.Server.Shared.Resources;
using Unity;

namespace Core.Server.Web.Controllers
{
    public class InnerRestController<TCreateResource, TResource>
        : ControllerBase
        where TCreateResource : CreateResource
        where TResource : Resource
    {
        [Dependency]
        public IInnerRestApplication<TCreateResource, TResource> Application { get; set; }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TResource>>> Get(string parentId)
        {
            SetUser();
            return await Application.Get(parentId);
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TResource>> Get(string parentId, string id)
        {
            SetUser();
            return await Application.Get(parentId, id);
        }

        [HttpPost]
        public virtual async Task<ActionResult<TResource>> Create(string parentId, TCreateResource resource)
        {
            SetUser();
            return await Application.Create(parentId, resource);
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult<TResource>> Update(string parentId, string id, TCreateResource resource)
        {
            SetUser();
            return await Application.Update(parentId, id, resource);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<TResource>> Delete(string parentId, string id)
        {
            SetUser();
            return await Application.Delete(parentId, id);
        }

        private void SetUser()
        {
            //Application.CurrentUser = GetUser();
        }
    }
}
