using Core.Server.Application;
using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Common.Validators
{
    [Inject]
    public class ResourceValidator<TAlterResource, TEntity>
        : BaseApplication<TEntity>,
          IResourceValidator<TAlterResource, TEntity>
        where TEntity : Entity
    {
        public virtual async Task<ActionResult> ValidateCreate(TAlterResource createResource)
        {
            return Ok();
        }

        public virtual async Task<ActionResult> ValidateCreate(TAlterResource createResource, TEntity entity)
        {
            return Ok();
        }

        public virtual async Task<ActionResult> ValidateUpdate(TAlterResource updateResource, TEntity entity)
        {
            return Ok();
        }
    }
}
