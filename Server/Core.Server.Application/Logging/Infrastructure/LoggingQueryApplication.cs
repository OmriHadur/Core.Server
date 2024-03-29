﻿using Core.Server.Common.Applications;
using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Shared.Query;
using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application.Logging
{
    [Inject(2)]
    public class LoggingQueryApplication<TResource, TEntity>
        : LoggingApplication<QueryApplication<TResource, TEntity>>
        , IQueryApplication<TResource>
        where TResource : Resource
        where TEntity : Entity
    {
        public Task<ActionResult<IEnumerable<TResource>>> Query(QueryResource queryResource)
        {
            return LogginCall(() => Application.Query(queryResource), queryResource);
        }
    }
}
