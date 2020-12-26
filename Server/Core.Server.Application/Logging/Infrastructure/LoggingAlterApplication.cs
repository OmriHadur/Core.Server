using Core.Server.Common.Applications;
using Core.Server.Common.Entities;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application.Logging
{
    [Inject(2)]
    public class LoggingAlterApplication<TAlterResource,TResource, TEntity>
        : LoggingApplication<AlterApplication<TAlterResource, TResource, TEntity>>
        , IAlterApplication<TAlterResource, TResource>
        where TResource : Resource
        where TEntity : Entity
    {
        public Task<ActionResult<TResource>> Create(TAlterResource resource)
        {
            return LogginCall(() => Application.Create(resource), resource);
        }

        public Task<ActionResult> Delete(string id)
        {
            return LogginCall(() => Application.Delete(id), id);
        }

        public Task<ActionResult> DeleteAll()
        {
            return LogginCall(() => Application.DeleteAll());
        }

        public Task<ActionResult<TResource>> Replace(string id, TAlterResource resource)
        {
            return LogginCall(() => Application.Replace(id, resource), resource);
        }

        public Task<ActionResult<TResource>> Update(string id, TAlterResource resource)
        {
            return LogginCall(() => Application.Update(id, resource), resource);
        }
    }
}
