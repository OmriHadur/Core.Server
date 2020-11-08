using Core.Server.Common.Applications;
using Core.Server.Common.Entities;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Unity;
using System.Collections.Generic;

namespace Core.Server.Application.Logging
{
    [Inject(2)]
    public class LoggingBatchApplication<TCreateResource, TUpdateResource, TResource, TEntity>
        : LoggingApplication<BatchApplication<TCreateResource, TUpdateResource, TResource, TEntity>>
        , IBatchApplication<TCreateResource, TUpdateResource, TResource>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
        where TEntity : Entity
    {
        public Task<ActionResult<IEnumerable<TResource>>> BatchCreate(TCreateResource[] resources)
        {
            return CallApplicationWithLog(() => Application.BatchCreate(resources), resources);
        }

        public Task<ActionResult<IEnumerable<string>>> BatchDelete(string[] ids)
        {
            return CallApplicationWithLog(() => Application.BatchDelete(ids), ids);
        }

        public Task<ActionResult<IEnumerable<TResource>>> BatchUpdate(TUpdateResource[] resources)
        {
            return CallApplicationWithLog(() => Application.BatchUpdate(resources), resources);
        }
    }
}
