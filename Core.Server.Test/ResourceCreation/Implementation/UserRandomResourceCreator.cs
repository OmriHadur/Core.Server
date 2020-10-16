using Core.Server.Shared.Resources.Users;
using Core.Server.Tests.Configuration;
using Unity;

namespace Core.Server.Test.ResourceCreation
{
    public class UserRandomResourceCreator
        : RandomResourceCreator<UserCreateResource, UserUpdateResource, UserResource>
    {
        [Dependency]
        public TestConfig Config;

        protected override void AddRandomValues(UserCreateResource createResource)
        {
            base.AddRandomValues(createResource);
            createResource.Password = Config.UserPassword;
        }
    }
}