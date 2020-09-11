using RestApi.Client.Interfaces;
using RestApi.Shared.Resources.Users;

namespace RestApi.Client.Clients
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
