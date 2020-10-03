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
    public class QueryController<TResource, TApplication>
        : ControllerBase<TApplication>
        where TResource : Resource
        where TApplication: IQueryApplication<TResource>
    {
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

        [HttpHead("{id}")]
        public virtual async Task<ActionResult<TResource>> Exists(string id)
        {
            return await Application.Exists(id);
        }
    }
}
