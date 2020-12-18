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
            createResource.Password = Config.DefaultPassword;
            createResource.RolesIds = new string[0];
        }

        protected override void AddRandomValues(UserCreateResource createResource, UserResource existingResource)
        {
            base.AddRandomValues(createResource, existingResource);
            createResource.Email = existingResource.Email;
            createResource.Password = Config.DefaultPassword;
            createResource.RolesIds = new string[0];
        }
    }
}