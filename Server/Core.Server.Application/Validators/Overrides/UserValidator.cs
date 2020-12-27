using Core.Server.Common;
using Core.Server.Common.Attributes;
using Core.Server.Common.Config;
using Core.Server.Common.Entities;
using Core.Server.Common.Validators;
using Core.Server.Shared.Resources.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application.Validators.Implementation
{
    [Inject]
    public class UserValidator : ResourceValidator<UserAlterResource, UserEntity>
    {
        [Dependency]
        public AppSettings AppSettings;

        [Dependency]
        public IEntityValidator<RoleEntity> RoleEntityValidator;

        public override async Task<IEnumerable<StringKeyValuePair>> ValidateCreate(UserAlterResource createResource)
        {
            var validation = GetValidateCreate(createResource).ToList();
            if (await LookupRepository.Exists(e => e.Email == createResource.Email))
                AddValidation(validation, nameof(UserAlterResource.Email), "Email already exists");
            await AddNotFoundRoles(createResource, validation);
            return validation;
        }

        protected override async Task<IEnumerable<StringKeyValuePair>> ValidateAlter(UserAlterResource createResource, UserEntity entity)
        {
            var validation = new List<StringKeyValuePair>();
            AddAuthorized(entity, validation);
            await AddNotFoundRoles(createResource, validation);
            return validation;
        }

        private void AddAuthorized(UserEntity entity, List<StringKeyValuePair> validation)
        {
            if (entity.Id != CurrentUser.Id &&
                CurrentUser.Email != AppSettings.AdminUserName)
                AddValidation(validation, nameof(UserAlterResource.Email), "Unauthorized to alter user");
        }

        private async Task AddNotFoundRoles(UserAlterResource userAlterResource, List<StringKeyValuePair> validation)
        {
            var notFound = await RoleEntityValidator.ValidateFound(userAlterResource.RolesIds, nameof(userAlterResource.RolesIds));
            validation.AddRange(notFound);
        }
    }
}