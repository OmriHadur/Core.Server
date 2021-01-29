using Core.Server.Common;
using Core.Server.Common.Attributes;
using Core.Server.Common.Config;
using Core.Server.Common.Entities;
using Core.Server.Common.Validators;
using Core.Server.Shared.Resources;
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
            var validation = (await base.ValidateCreate(createResource)).ToList();
            if (await LookupRepository.Exists(e => e.Email == createResource.Email))
                AddValidationAlreadyExists(validation, nameof(UserAlterResource.Email));
            return validation;
        }

        protected override async Task<IEnumerable<StringKeyValuePair>> ValidateAlter(UserAlterResource alterResource, UserEntity entity)
        {
            var validation = (await base.ValidateAlter(alterResource, entity)).ToList();
            AddAuthorized(entity, validation);
            return validation;
        }

        private void AddAuthorized(UserEntity entity, List<StringKeyValuePair> validation)
        {
            if (entity.Id != CurrentUser.Id && CurrentUser.Email != AppSettings.AdminUserName)
                AddValidationUnauthorized(validation, nameof(UserAlterResource.Email));
        }
    }
}