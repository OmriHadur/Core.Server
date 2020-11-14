using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources.Users;
using Core.Server.Tests.Configuration;
using Core.Server.Tests.Utils;
using Unity;

namespace Core.Server.Test.ResourceCreation
{
    [Inject]
    public class UserRandomResourceCreator
        : RandomResourceCreator<UserCreateResource, UserUpdateResource, UserResource>
    {
        [Dependency]
        public TestConfig Config;

        [Dependency]
        public ICurrentUser CurrentUser;

        protected override void AddRandomValues(UserCreateResource createResource)
        {
            base.AddRandomValues(createResource);
            createResource.Password = Config.UserPassword;
        }

        protected override void AddRandomValues(UserCreateResource createResource, UserResource existingResource)
        {
            base.AddRandomValues(createResource, existingResource);
            createResource.Password = Config.UserPassword + Config.UserPassword;
            createResource.Email = existingResource.Email;
            CurrentUser.Relogin();
        }
    }
}