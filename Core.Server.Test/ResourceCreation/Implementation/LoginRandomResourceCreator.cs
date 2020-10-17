using Core.Server.Injection.Attributes;
using Core.Server.Shared.Resources.Users;
using Core.Server.Tests.Configuration;
using Core.Server.Tests.ResourceCreators.Interfaces;
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
        public IResourceCreate<UserResource> userResourceHandler;

        protected override void AddRandomValues(LoginCreateResource createResource)
        {
            createResource.Email = userResourceHandler.GetOrCreate().Email;
            createResource.Password = Config.UserPassword;
        }

        protected override void AddRandomValues(LoginUpdateResource updateResource)
        {
            updateResource.Email = userResourceHandler.GetOrCreate().Email;
            updateResource.Password = Config.UserPassword;
        }
    }
}