using RestApi.Client.Interfaces;
using RestApi.Shared.Resources.Users;

namespace RestApi.Client.Clients
{
    public class LoginControllClient : 
        RestClient<LoginCreateResource, LoginResource>,
        ILoginControllClient
    {
        public LoginControllClient()
            :base("logins")
        {
        }
    }
}
