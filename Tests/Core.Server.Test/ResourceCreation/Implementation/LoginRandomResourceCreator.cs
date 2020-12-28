using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using Core.Server.Shared.Resources.Users;
using Core.Server.Test.Configuration;
using Core.Server.Test.ResourceCreators.Interfaces;
using Core.Server.Test.Utils;
using Unity;

namespace Core.Server.Test.ResourceCreation
{
    [Inject]
    public class LoginRandomResourceCreator
        : RandomResourceCreator<LoginAlterResource, LoginResource>
    {
        [Dependency]
        public TestConfig Config;

        [Dependency]
        public IResourceCreate<UserResource> UserResourceCreate;

        [Dependency]
        public ICurrentUser CurrentUser;

        protected override void AddRandomValues(LoginAlterResource createResource)
        {
            createResource.Email = UserResourceCreate.GetOrCreate().Email;
            createResource.Password = Config.DefaultPassword;
        }
        protected override void AddRandomToExistingValues(LoginAlterResource alterResource, LoginResource existingResource)
        {
            base.AddRandomToExistingValues(alterResource, existingResource);
        }
    }
}