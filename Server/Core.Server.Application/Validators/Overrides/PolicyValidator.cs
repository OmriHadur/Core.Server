using Core.Server.Common;
using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Core.Server.Common.Validators;
using Core.Server.Shared.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application.Validators.Implementation
{
    [Inject]
    public class PolicyValidator : ResourceValidator<PolicyAlterResource, PolicyEntity>
    {
        [Dependency]
        public ILookupRepository<PolicyEntity> PolicyLookupRepository { get; set; }

        public override async Task<IEnumerable<StringKeyValuePair>> ValidateCreate(PolicyAlterResource createResource)
        {
            var validation = (await base.ValidateCreate(createResource)).ToList();
            var type = Type.GetType(createResource.ResourceType);
            if (type == null)
                AddValidationInvalid(validation, nameof(createResource.ResourceType));
            return validation;
        }
    }
}