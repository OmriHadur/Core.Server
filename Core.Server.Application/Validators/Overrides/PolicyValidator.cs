using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Core.Server.Common.Validators;
using Core.Server.Shared.Errors;
using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application.Validators.Implementation
{
    [Inject]
    public class PolicyValidator
        : ResourceValidator<PolicyCreateResource,PolicyUpdateResource,PolicyEntity>
    {
        [Dependency]
        public ILookupRepository<PolicyEntity> PolicyLookupRepository { get; set; }

        public async override Task<ActionResult> Validate(PolicyCreateResource createResource)
        {
            var assemblyName = typeof(Resource).Assembly.FullName;
            var type = Type.GetType(createResource.ResourceType + "," + assemblyName);
            if (type == null)
                return BadRequest(BadRequestReason.InvalidResource);
            return Ok();
        }
    }
}
