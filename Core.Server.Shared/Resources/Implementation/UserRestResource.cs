using Core.Server.Shared.Resources.Base;
using Core.Server.Shared.Resources.Users;

namespace Core.Server.Shared.Resources.Implementation
{
    public class UserRestResource:
        RestResource<UserCreateResource,UserUpdateResource,UserResource>
    {
    }
}
