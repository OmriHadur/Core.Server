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
    public class QueryController<TResource>
        : ControllerBase
        where TResource : Resource
    {
        [Dependency]
        public IQueryApplication<TResource> QueryApplication { get; set; }

        [Dependency]
        public ILogger<TResource> Logger;

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TResource>>> Get()
        {
            return await QueryApplication.Get();
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TResource>> Get(string id)
        {
            return await QueryApplication.Get(id);
        }

        [HttpPost("query")]
        public virtual async Task<ActionResult<IEnumerable<TResource>>> Query(QueryResource query)
        {
            return await QueryApplication.Query(query);
        }

        [HttpHead("{id}")]
        public virtual async Task<ActionResult<TResource>> Exists(string id)
        {
            return await QueryApplication.Exists(id);
        }

        protected void SetUser()
        {
            QueryApplication.CurrentUser = GetUser();
        }
    }
}
