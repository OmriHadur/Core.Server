using RestApi.Shared.Resources.Users;

namespace RestApi.Client.Interfaces
{
    public interface ILoginControllClient : IRestClient<LoginCreateResource, LoginResource>
    {
    }
}
