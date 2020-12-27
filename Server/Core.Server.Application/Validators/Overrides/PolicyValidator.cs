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

        public override async Task<IEnumerable<StringKeyValuePair>> ValidateCreate(PolicyAlterResource alterResource)
        {
            var validation = GetValidateCreate(alterResource).ToList();
            var type = Type.GetType(alterResource.ResourceType);
            if (type == null)
                AddValidation(validation,nameof(alterResource.ResourceType), "Invalid resource");
            return validation;
        }
    }
}