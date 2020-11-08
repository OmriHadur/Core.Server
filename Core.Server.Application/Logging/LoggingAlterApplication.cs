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
    public class LoggingAlterApplication<TCreateResource, TUpdateResource,TResource, TEntity>
        : LoggingApplication<AlterApplication<TCreateResource, TUpdateResource, TResource, TEntity>>
        , IAlterApplication<TCreateResource, TUpdateResource, TResource>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
        where TEntity : Entity
    {
        public Task<ActionResult<TResource>> Create(TCreateResource resource)
        {
            return CallApplicationWithLog(() => Application.Create(resource), resource);
        }

        public Task<ActionResult> Delete(string id)
        {
            return CallApplicationWithLog(() => Application.Delete(id), id);
        }

        public Task<ActionResult> DeleteAll()
        {
            return CallApplicationWithLog(() => Application.DeleteAll());
        }

        public Task<ActionResult<TResource>> Replace(string id, TCreateResource resource)
        {
            return CallApplicationWithLog(() => Application.Replace(id, resource), id,resource);
        }

        public Task<ActionResult<TResource>> Update(string id, TUpdateResource resource)
        {
            return CallApplicationWithLog(() => Application.Update(id, resource), id, resource);
        }
    }
}
