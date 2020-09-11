
using RestApi.Shared.Resources.Users;

namespace RestApi.Tests.RestResourcesCreators
{
    public class LoginResourceCreator : RestResourceCreator<LoginCreateResource, LoginResource>
    {
        public override void SetCreateResource(LoginCreateResource createResource)
        {
            createResource.Email = GetResource<UserResource>().Email;
            createResource.Password = ConfigHandler.Config.UserPassword;
        }
    }
}