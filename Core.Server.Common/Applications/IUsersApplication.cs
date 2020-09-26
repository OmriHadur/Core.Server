using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Server.Shared.Resources.Users;

namespace Core.Server.Common.Applications
{
    public interface IUsersApplication : IRestApplication<UserCreateResource, UserResource>
    {
    }
}
