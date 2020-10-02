
using Core.Server.Shared.Resources.Users;

namespace Core.Server.Tests.ResourceCreators
{
    public class LoginResourceCreator : RestResourceCreator<LoginCreateResource,LoginUpdateResource, LoginResource>
    {
        public override void SetCreateResource(LoginCreateResource createResource)
        {
            createResource.Email = GetResource<UserResource>().Email;
            createResource.Password = ConfigHandler.Config.UserPassword;
        }
    }
}