using Core.Server.Application;
using Core.Server.Common.Entities;
using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Server.Injection.Attributes;

namespace Core.Server.Common.Validators
{
    [Inject]
    public class ResourceValidator<TCreateResource, TUpdateResource, TEntity>
        : BaseApplication,
          IResourceValidator<TCreateResource, TUpdateResource, TEntity>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TEntity : Entity
    {
        public virtual async Task<ActionResult> Validate(TCreateResource createResource)
        {
            return Ok();
        }

        public virtual async Task<ActionResult> Validate(TCreateResource createResource, TEntity entity)
        {
            return Ok();
        }

        public virtual async Task<ActionResult> Validate(TUpdateResource updateResource, TEntity entity)
        {
            return Ok();
        }
    }
}
