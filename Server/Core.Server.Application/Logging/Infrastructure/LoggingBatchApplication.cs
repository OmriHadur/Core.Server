using Core.Server.Common.Applications;
using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application.Logging
{
    [Inject(2)]
    public class LoggingBatchApplication<TAlterResource, TResource, TEntity>
        : LoggingApplication<BatchApplication<TAlterResource, TResource, TEntity>>
        , IBatchApplication<TAlterResource, TResource>
        where TResource : Resource
        where TEntity : Entity
    {
        public Task<ActionResult<IEnumerable<TResource>>> BatchCreate(TAlterResource[] resources)
        {
            return LogginCall(() => Application.BatchCreate(resources), resources);
        }

        public Task<ActionResult<IEnumerable<string>>> BatchDelete(string[] ids)
        {
            return LogginCall(() => Application.BatchDelete(ids), ids);
        }

        public Task<ActionResult<IEnumerable<TResource>>> BatchUpdate(TAlterResource[] resources)
        {
            return LogginCall(() => Application.BatchUpdate(resources), resources);
        }
    }
}
