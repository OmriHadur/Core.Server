using Core.Server.Application.Helpers;
using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Mappers;
using Core.Server.Common.Repositories;
using Core.Server.Common.Validators;
using Core.Server.Shared.Resources.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application.Validators.Implementation
{
    [Inject]
    public class LoginValidator
        : ResourceValidator<LoginCreateResource,LoginUpdateResource,LoginEntity>
    {
        private readonly PasswordHasher _passwordHasher = new PasswordHasher();

        [Dependency]
        public ILookupRepository<UserEntity> UserLookupRepository { get; set; }


        public override async Task<ActionResult> Validate(LoginCreateResource createResource)
        {
            var userEntity = await UserLookupRepository.FindFirst(e => e.Email == createResource.Email);
            if (userEntity == null || !IsPasswordCurrent(createResource, userEntity))
                return Unauthorized();
            return await base.Validate(createResource);
        }

        private bool IsPasswordCurrent(LoginCreateResource resource, UserEntity user)
        {
            return _passwordHasher.VerifyPasswordHash(resource.Password, user.PasswordHash, user.PasswordSalt);
        }
    }
}
