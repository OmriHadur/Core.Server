using Core.Server.Shared.Resources.Users;

namespace Core.Server.Common.Applications
{
    public interface IUserApplication : 
        IBatchApplication<UserCreateResource, UserUpdateResource, UserResource>
    {
    }
}
