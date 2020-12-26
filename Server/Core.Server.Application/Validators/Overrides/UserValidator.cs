using Core.Server.Common.Attributes;
using Core.Server.Common.Config;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
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
        : ResourceValidator<UserCreateResource, UserUpdateResource, UserEntity>
    {
        [Dependency]
        public AppSettings AppSettings;

        [Dependency]
        public ILookupRepository<RoleEntity> RolesLookupRepository;

        public async override Task<ActionResult> Validate(UserCreateResource createResource)
        {
            if (await LookupRepository.Exists(e => e.Email == createResource.Email))
                return BadRequest(BadRequestReason.SameExists);
            var notFoundId = await RolesLookupRepository.GetNotFoundId(createResource.RolesIds);
            if (notFoundId != null)
                return NotFound(notFoundId);
            return Ok();
        }

        public async override Task<ActionResult> Validate(UserCreateResource createResource, UserEntity entity)
        {
            if (createResource.Email != entity.Email)
                return BadRequest(BadRequestReason.Unchangeable);
            if (createResource.Email != CurrentUser.Email && 
                CurrentUser.Email != AppSettings.AdminUserName)
                return Unauthorized();
            var notFoundId = await RolesLookupRepository.GetNotFoundId(createResource.RolesIds);
            if (notFoundId != null)
                return NotFound(notFoundId);
            return Ok();
        }
    }
}