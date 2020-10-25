﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Server.Common.Applications;
using Core.Server.Shared.Resources;
using Core.Server.Shared.Query;
using Core.Server.Injection.Attributes;

namespace Core.Server.Web.Controllers
{
    [InjectBoundleController]
    public class QueryController<TResource>
        : BaseController<IQueryApplication<TResource>>
        where TResource : Resource
    {
        [HttpPost("query")]
        public virtual async Task<ActionResult<IEnumerable<TResource>>> Query(QueryResource query)
        {
            return await Application.Query(query);
        }
    }
}
