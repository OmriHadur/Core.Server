using Core.Server.Application.Helpers;
using Core.Server.Common;
using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Core.Server.Common.Validators;
using Core.Server.Shared.Resources.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application.Validators.Implementation
{
    [Inject]
    public class LoginValidator
        : ResourceValidator<LoginAlterResource,LoginEntity>
    {
        private readonly PasswordHasher _passwordHasher = new PasswordHasher();

        [Dependency]
        public ILookupRepository<UserEntity> UserLookupRepository { get; set; }

        public override async Task<IEnumerable<StringKeyValuePair>> ValidateCreate(LoginAlterResource createResource)
        {
            var validation = GetValidateCreate(createResource).ToList();
            var userEntity = await UserLookupRepository.FindFirst(e => e.Email == createResource.Email);
            if (userEntity == null || !IsPasswordCurrent(createResource, userEntity))
                AddValidation(validation, nameof(createResource.Email), "Wrong email or password");
            return validation;
        }

        private bool IsPasswordCurrent(LoginAlterResource resource, UserEntity user)
        {
            return _passwordHasher.VerifyPasswordHash(resource.Password, user.PasswordHash, user.PasswordSalt);
        }
    }
}
