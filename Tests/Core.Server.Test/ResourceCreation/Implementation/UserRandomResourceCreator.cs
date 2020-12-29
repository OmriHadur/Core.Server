using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources.User;
using Core.Server.Test.Configuration;
using Core.Server.Test.Utils;
using Unity;

namespace Core.Server.Test.ResourceCreation
{
    [Inject]
    public class UserRandomResourceCreator
        : RandomResourceCreator<UserAlterResource, UserResource>
    {
        [Dependency]
        public TestConfig Config;

        [Dependency]
        public ICurrentUser CurrentUser;

        protected override void AddRandomValues(UserAlterResource createResource)
        {
            base.AddRandomValues(createResource);
            createResource.Password = Config.DefaultPassword;
        }

        protected override void AddRandomToExistingValues(UserAlterResource createResource, UserResource existingResource)
        {
            base.AddRandomToExistingValues(createResource, existingResource);
            createResource.Password = Config.DefaultPassword;
        }
    }
}