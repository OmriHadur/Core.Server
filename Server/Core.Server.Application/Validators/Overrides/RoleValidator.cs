using Core.Server.Common;
using Core.Server.Common.Attributes;
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
    public class RoleValidator : ResourceValidator<RoleAlterResource, RoleEntity>
    {
        [Dependency]
        public IEntityValidator<PolicyEntity> PolicyEntityValidator { get; set; }

        public override async Task<IEnumerable<StringKeyValuePair>> ValidateCreate(RoleAlterResource alterResource)
        {
            var validation = (await base.ValidateCreate(alterResource)).ToList();
            var notFound = await GetNotFoundPolicies(alterResource);
            validation.AddRange(notFound);
            return validation;
        }

        protected override Task<IEnumerable<StringKeyValuePair>> ValidateAlter(RoleAlterResource alterResource, RoleEntity entity)
        {
            return GetNotFoundPolicies(alterResource);
        }

        private Task<IEnumerable<StringKeyValuePair>> GetNotFoundPolicies(RoleAlterResource alterResource)
        {
            return PolicyEntityValidator.ValidateFound(alterResource.PoliciesId, nameof(alterResource.PoliciesId));
        }
    }
}
