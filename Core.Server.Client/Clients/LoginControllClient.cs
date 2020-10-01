using Core.Server.Client.Interfaces;
using Core.Server.Shared.Resources.Users;

namespace Core.Server.Client.Clients
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
