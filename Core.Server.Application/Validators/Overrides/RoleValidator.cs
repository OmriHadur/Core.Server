using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Core.Server.Common.Validators;
using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application.Validators.Implementation
{
    [Inject]
    public class RoleValidator
        : ResourceValidator<RoleCreateResource,RoleUpdateResource,RoleEntity>
    {
        [Dependency]
        public ILookupRepository<PolicyEntity> PolicyLookupRepository { get; set; }

        public async override Task<ActionResult> Validate(RoleCreateResource createResource)
        {
            var policies = await PolicyLookupRepository.Get(createResource.PoliciesId);
            var notFoundId = createResource.PoliciesId.FirstOrDefault(id => !policies.Any(e => e?.Id == id));
            if (notFoundId != null)
                return NotFound(notFoundId);
            return Ok();
        }
    }
}
