using RestApi.Standard.Client.Interfaces;
using RestApi.Standard.Shared.Resources.Users;

namespace RestApi.Standard.Client.Clients
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
