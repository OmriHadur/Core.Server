using Core.Server.Common.Applications;
using Core.Server.Common.Entities;
using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core.Server.Common.Validators
{
    public interface IResourceValidator<TCreateResource, TUpdateResource, TEntity>
       : IBaseApplication
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TEntity : Entity
    {
        Task<ActionResult> Validate(TCreateResource createResource);

        Task<ActionResult> Validate(TCreateResource createResource, TEntity entity);

        Task<ActionResult> Validate(TUpdateResource updateResource, TEntity entity);
    }
}
