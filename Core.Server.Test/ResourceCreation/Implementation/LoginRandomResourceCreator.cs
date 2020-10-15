using Core.Server.Shared.Resources.Users;
using Core.Server.Tests.Configuration;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Unity;

namespace Core.Server.Test.ResourceCreation
{
    public class LoginRandomResourceCreator
        : RandomResourceCreator<LoginCreateResource, LoginUpdateResource, LoginResource>
    {
        [Dependency]
        public IConfigHandler ConfigHandler;

        [Dependency]
        public IResourceCreate<UserResource> userResourceHandler;

        protected override void AddRandomValues(LoginUpdateResource updateResource,LoginResource existingResource)
        {
            updateResource.Email = userResourceHandler.GetOrCreate().Email;
            updateResource.Password = ConfigHandler.Config.UserPassword;
        }
    }
}