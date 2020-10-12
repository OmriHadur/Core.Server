using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Server.Common.Applications;
using Core.Server.Shared.Resources;
using Core.Server.Shared.Query;
using Unity;
using Core.Server.Common.Attributes;

namespace Core.Server.Web.Controllers
{
    [InjectBoundleController]
    public class QueryController<TResource>
        : BaseController<IQueryApplication<TResource>>
        where TResource : Resource
    {
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
