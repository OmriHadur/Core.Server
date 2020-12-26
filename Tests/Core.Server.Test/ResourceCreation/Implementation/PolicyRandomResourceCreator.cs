using Core.Server.Common.Attributes;
using Core.Server.Injection.Interfaces;
using Core.Server.Shared.Resources;
using Unity;

namespace Core.Server.Test.ResourceCreation
{
    [Inject]
    public class PolicyRandomResourceCreator
        : RandomResourceCreator<PolicyAlterResource, PolicyResource>
    {
        [Dependency]
        public IReflactionHelper ReflactionHelper;

        protected override void AddRandomCreateValues(PolicyAlterResource createResource)
        {
            base.AddRandomCreateValues(createResource);
            var fullName = ReflactionHelper.GetTypeFullName(typeof(ExampleResource));
            createResource.ResourceType = fullName;
            createResource.ResourceActions = ResourceActions.All;
        }
    }
}