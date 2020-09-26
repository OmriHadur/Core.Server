using Core.Server.Client.Results;
using Core.Server.Shared.Resources.Users;
using System.Threading.Tasks;

namespace Core.Server.Client.Interfaces
{
    public interface IUserControllClient : IRestClient<UserCreateResource, UserResource>
    {
    }
}
