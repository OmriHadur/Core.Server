using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Unity;

namespace Core.Server.Test.ResourceCreation
{
    [Inject]
    public class RoleRandomResourceCreator
        : RandomResourceCreator<RoleCreateResource, RoleUpdateResource, RoleResource>
    {
        [Dependency]
        public IResourceCreate<PolicyResource> PolicyResourceCreate;

        protected override void AddRandomValues(RoleCreateResource createResource)
        {
            base.AddRandomValues(createResource);
            var policyId = PolicyResourceCreate.GetOrCreate().Id;
            createResource.PoliciesId = new string[] { policyId };
        }
    }
}