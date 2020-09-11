using System.Net.Http;
using System.Threading.Tasks;
using RestApi.Client.Interfaces;
using RestApi.Client.Results;
using RestApi.Shared.Resources.Users;

namespace RestApi.Client.Clients
{
    public class UserControllClient : 
        RestClient<UserCreateResource, UserResource>,
        IUserControllClient
    {
        public UserControllClient()
            :base("users")
        {
        }
    }
}
