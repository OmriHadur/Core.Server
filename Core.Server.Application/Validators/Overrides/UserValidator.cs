using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Validators;
using Core.Server.Shared.Errors;
using Core.Server.Shared.Resources.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application.Validators.Implementation
{
    [Inject]
    public class UserValidator
        : ResourceValidator<UserCreateResource,UserUpdateResource,UserEntity>
    {
        public async override Task<ActionResult> Validate(UserCreateResource createResource, UserEntity entity)
        {
            if (createResource.Email != entity.Email)
                return BadRequest(BadRequestReason.Unchangeable);
            if (createResource.Email != CurrentUser.Email)
                return Unauthorized();
            return Ok();
        }
    }
}
