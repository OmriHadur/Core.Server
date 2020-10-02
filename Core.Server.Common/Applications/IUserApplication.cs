using Core.Server.Shared.Resources.Users;

namespace Core.Server.Common.Applications
{
    public interface IUserApplication : 
        IRestApplication<UserCreateResource, UserUpdateResource, UserResource>
    {
    }
}
