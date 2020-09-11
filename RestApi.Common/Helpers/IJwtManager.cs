using RestApi.Shared.Resources.Users;
using System.Security.Claims;

namespace RestApi.Common.Entities.Helpers
{
    public interface IJwtManager
    {
        string GenerateToken(UserResource user,string secret);
        UserResource GetUser(ClaimsPrincipal ClaimsPrincipal);
    }
}