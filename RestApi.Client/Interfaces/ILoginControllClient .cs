using RestApi.Standard.Client.Results;
using RestApi.Standard.Shared.Resources.Users;
using System.Threading.Tasks;

namespace RestApi.Standard.Client.Interfaces
{
    public interface ILoginControllClient : IRestClient<LoginCreateResource, LoginResource>
    {
    }
}
