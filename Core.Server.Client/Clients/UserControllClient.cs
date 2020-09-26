using Core.Server.Client.Interfaces;
using Core.Server.Shared.Resources.Users;

namespace Core.Server.Client.Clients
{
    public class UserControllClient : 
        RestClient<UserCreateResource, UserResource>,
        IUserControllClient
    {
        public UserControllClient()
            :base("user")
        {
        }
    }
}
