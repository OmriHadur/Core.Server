using System.Net.Http;
using System.Threading.Tasks;
using RestApi.Standard.Client.Interfaces;
using RestApi.Standard.Client.Results;
using RestApi.Standard.Shared.Resources.Users;

namespace RestApi.Standard.Client.Clients
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
