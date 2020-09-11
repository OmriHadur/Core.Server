using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RestApi.Standard.Shared.Resources.Users;

namespace RestApi.Common.Applications
{
    public interface IUsersApplication : IRestApplication<UserCreateResource, UserResource>
    {
    }
}
