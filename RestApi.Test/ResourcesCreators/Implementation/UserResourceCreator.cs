using RestApi.Shared.Resources.Users;

namespace RestApi.Tests.RestResourcesCreators
{
    public class UserResourceCreator : RestResourceCreator<UserCreateResource, UserResource>
    {
        public override void SetCreateResource(UserCreateResource createResource)
        {
            base.SetCreateResource(createResource);
            createResource.Password = ConfigHandler.Config.UserPassword;
        }
    }
}
