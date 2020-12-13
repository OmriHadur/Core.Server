using Core.Server.Common.Applications;
using Core.Server.Common.Entities;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application.Logging
{
    [Inject(2)]
    public class LoggingOwnedApplication<TResource, TEntity>
        : LoggingApplication<OwnedApplication<TResource, TEntity>>
        , IOwnedApplication<TResource>
        where TResource : Resource
        where TEntity : OwnedEntity
    {
        public Task<ActionResult> Any()
        {
          return  LogginCall(() => Application.Any());
        }

        public Task<ActionResult<IEnumerable<TResource>>> GetAllOwned()
        {
            return LogginCall(() => Application.GetAllOwned());
        }

        public Task<ActionResult> Reassign(ReassginResource reassginResource)
        {
            return LogginCall(() => Application.Reassign(reassginResource));
        }
    }
}
