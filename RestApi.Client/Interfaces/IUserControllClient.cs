using RestApi.Client.Results;
using RestApi.Shared.Resources.Users;
using System.Threading.Tasks;

namespace RestApi.Client.Interfaces
{
    public interface IUserControllClient : IRestClient<UserCreateResource, UserResource>
    {
    }
}
