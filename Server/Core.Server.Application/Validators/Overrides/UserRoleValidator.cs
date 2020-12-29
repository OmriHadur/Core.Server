using Core.Server.Common;
using Core.Server.Common.Attributes;
using Core.Server.Common.Config;
using Core.Server.Common.Entities;
using Core.Server.Common.Validators;
using Core.Server.Shared.Resources.User;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application.Validators.Implementation
{
    [Inject]
    public class UserRoleValidator : ResourceValidator<UserRoleAlterResource, UserEntity>
    {
        [Dependency]
        public AppSettings AppSettings;

        [Dependency]
        public IEntityValidator<RoleEntity> RoleEntityValidator;

        public override async Task<IEnumerable<StringKeyValuePair>> ValidateReplace(UserRoleAlterResource alterResource, UserEntity entity)
        {
            var validaion = new List<StringKeyValuePair>();
            var hadId = entity.Roles.Any(r => r.Id == alterResource.Id);
            if (hadId)
                AddValidationAlreadyExists(validaion, nameof(UserRoleAlterResource.Id));
            return validaion;
        }
    }
}