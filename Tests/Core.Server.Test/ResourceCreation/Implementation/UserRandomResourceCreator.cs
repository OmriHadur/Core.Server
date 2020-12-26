using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources.Users;
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

        protected override void AddRandomCreateValues(UserAlterResource createResource)
        {
            base.AddRandomCreateValues(createResource);
            createResource.Password = Config.DefaultPassword;
            createResource.RolesIds = new string[0];
        }

        protected override void AddRandomValues(UserAlterResource createResource, UserResource existingResource)
        {
            base.AddRandomValues(createResource, existingResource);
            createResource.Email = existingResource.Email;
            createResource.Password = Config.DefaultPassword;
            createResource.RolesIds = new string[0];
        }
    }
}