using Core.Server.Shared.Resources.Users;

namespace Core.Server.Client.Interfaces
{
    public interface ILoginControllClient : IRestClient<LoginCreateResource, LoginResource>
    {
    }
}
