using Core.Server.Common.Applications;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Query;
using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Server.Web.Controllers
{
    [InjectBoundleController]
    public class QueryController<TResource>
        : BaseController<IQueryApplication<TResource>>
        where TResource : Resource
    {
        [HttpPost("query")]
        public virtual async Task<ActionResult<IEnumerable<TResource>>> Query(QueryResource queryResource)
        {
            return await Application.Query(queryResource);
        }
    }
}
