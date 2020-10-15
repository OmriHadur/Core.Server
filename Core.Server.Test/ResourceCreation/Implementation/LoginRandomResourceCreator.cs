using Core.Server.Shared.Resources.Users;
using Core.Server.Test.ResourcesCreators.Infrastructure;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Core.Server.Tests.Utils;
using Unity;

namespace Core.Server.Test.ResourceCreation
{
    public class LoginRandomResourceCreator
        : RandomResourceCreator<LoginCreateResource, LoginUpdateResource>
    {
        [Dependency]
        public IConfigHandler ConfigHandler;

        [Dependency]
        public IResourceAlter<UserResource> userResourceHandler;

        protected override void AddRandomValues(LoginCreateResource createResource)
        {
            createResource.Email = userResourceHandler.GetOrCreate().Email;
            createResource.Password = ConfigHandler.Config.UserPassword;
        }
    }
}