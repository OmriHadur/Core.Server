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

        protected override void AddRandomCreateValues(RoleAlterResource createResource)
        {
            base.AddRandomCreateValues(createResource);
            var policyId = PolicyResourceCreate.GetOrCreate().Id;
            createResource.PoliciesId = new string[] { policyId };
        }
    }
}