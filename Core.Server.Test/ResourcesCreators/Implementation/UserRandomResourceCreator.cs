using Core.Server.Shared.Resources.Users;
using Core.Server.Test.ResourcesCreators.Infrastructure;
using Core.Server.Tests.Utils;
using Unity;

namespace Core.Server.Test.ResourcesCreators.Implementation
{
    public class UserRandomResourceCreator
        : RandomResourceCreator<UserCreateResource, UserUpdateResource>
    {
        [Dependency]
        public IConfigHandler ConfigHandler;

        protected override void AddRandomValues(UserCreateResource createResource)
        {
            base.AddRandomValues(createResource);
            createResource.Password = ConfigHandler.Config.UserPassword;
        }
    }
}