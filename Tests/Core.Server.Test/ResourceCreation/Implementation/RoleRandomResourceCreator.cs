using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using Core.Server.Test.ResourceCreators.Interfaces;
using Unity;

namespace Core.Server.Test.ResourceCreation
{
    [Inject]
    public class RoleRandomResourceCreator
        : RandomResourceCreator<RoleAlterResource, RoleResource>
    {
        [Dependency]
        public IResourceCreate<PolicyResource> PolicyResourceCreate;

        protected override void AddRandomValues(RoleAlterResource createResource)
        {
            base.AddRandomValues(createResource);
            var policyId = PolicyResourceCreate.GetOrCreate().Id;
            createResource.PoliciesId = new string[] { policyId };
        }

        protected override void AddRandomToExistingValues(RoleAlterResource alterResource, RoleResource existingResource)
        {
            base.AddRandomToExistingValues(alterResource, existingResource);
            var policyId = PolicyResourceCreate.Create().Value.Id;
            alterResource.PoliciesId = new string[] { policyId };
        }
    }
}