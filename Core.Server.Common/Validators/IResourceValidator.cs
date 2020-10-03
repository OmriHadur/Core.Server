using Core.Server.Common.Entities;
using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core.Server.Common.Validators
{
    public interface IResourceValidator<TCreateResource, TUpdateResource, TEntity>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TEntity : Entity
    {
        Task<ActionResult> Validate(TCreateResource createResource);

        Task<ActionResult> Validate(TUpdateResource updateResource, TEntity entity);
    }
}
