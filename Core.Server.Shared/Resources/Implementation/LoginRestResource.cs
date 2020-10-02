using Core.Server.Shared.Resources.Base;
using Core.Server.Shared.Resources.Users;

namespace Core.Server.Shared.Resources.Implementation
{
    public class LoginRestResource:
        RestResource<LoginCreateResource,LoginUpdateResource,LoginResource>
    {
    }
}
