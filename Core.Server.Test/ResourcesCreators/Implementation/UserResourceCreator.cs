using Core.Server.Shared.Resources.Users;

namespace Core.Server.Tests.ResourceCreators
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
