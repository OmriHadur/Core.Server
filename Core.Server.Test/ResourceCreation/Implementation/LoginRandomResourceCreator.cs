using Core.Server.Injection.Attributes;
using Core.Server.Shared.Resources.Users;
using Core.Server.Tests.Configuration;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Core.Server.Tests.Utils;
using Unity;

namespace Core.Server.Test.ResourceCreation
{
    [Inject]
    public class LoginRandomResourceCreator
        : RandomResourceCreator<LoginCreateResource, LoginUpdateResource, LoginResource>
    {
        [Dependency]
        public TestConfig Config;

        [Dependency]
        public IResourceCreate<UserResource> UserResourceCreate;

        [Dependency]
        public ICurrentUser CurrentUser;

        protected override void AddRandomValues(LoginCreateResource createResource)
        {
            CurrentUser.Login();
            createResource.Email = CurrentUser.Email;
            createResource.Password = Config.UserPassword;
        }
    }
}