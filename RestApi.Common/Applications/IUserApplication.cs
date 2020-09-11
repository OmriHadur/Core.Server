using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RestApi.Shared.Resources.Users;

namespace RestApi.Common.Applications
{
    public interface IUserApplication : IRestApplication<UserCreateResource, UserResource>
    {
    }
}
