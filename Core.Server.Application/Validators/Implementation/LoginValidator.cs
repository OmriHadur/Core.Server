using Core.Server.Application.Helper;
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
        private PasswordHasher _passwordHasher = new PasswordHasher();

        [Dependency]
        public IQueryRepository<UserEntity> UserQueryRepository { get; set; }

        [Dependency]
        public IResourceMapper<UserResource, UserEntity> UserResourceMapper { get; set; }

        public override async Task<ActionResult> Validate(LoginCreateResource createResource)
        {
            var userEntity = await UserQueryRepository.FindFirst(e => e.Email == createResource.Email);
            if (userEntity == null || !IsPasswordCurrent(createResource, userEntity))
                return new UnauthorizedResult();
            return await base.Validate(createResource);
        }
        private bool IsPasswordCurrent(LoginCreateResource resource, UserEntity user)
        {
            return _passwordHasher.VerifyPasswordHash(resource.Password, user.PasswordHash, user.PasswordSalt);
        }
    }
}
