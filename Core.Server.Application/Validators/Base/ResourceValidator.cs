using Core.Server.Common.Entities;
using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core.Server.Common.Validators
{
    public class ResourceValidator<TCreateResource, TUpdateResource, TEntity>
        : IResourceValidator<TCreateResource, TUpdateResource, TEntity>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TEntity : Entity
    {
        public virtual async Task<ActionResult> Validate(TCreateResource createResource)
        {
            return new OkResult();
        }

        public virtual async Task<ActionResult> Validate(TUpdateResource updateResource, TEntity entity)
        {
            return new OkResult();
        }
    }
}
