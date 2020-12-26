using Core.Server.Common.Applications;
using Core.Server.Common.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core.Server.Common.Validators
{
    public interface IResourceValidator<TAlterResource, TEntity>
       : IBaseApplication
        where TEntity : Entity
    {
        Task<ActionResult> ValidateCreate(TAlterResource createResource);

        Task<ActionResult> ValidateCreate(TAlterResource createResource, TEntity entity);

        Task<ActionResult> ValidateUpdate(TAlterResource updateResource, TEntity entity);
    }
}
