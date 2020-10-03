using System.Threading.Tasks;
using Core.Server.Shared.Resources.Users;

namespace Core.Server.Common.Applications
{
    public interface ILoginApplication 
        : IBatchApplication<LoginCreateResource, LoginUpdateResource, LoginResource>
    {
        Task DeleteByUserId(string id);
    }
}
