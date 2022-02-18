using Core.Server.Shared.Resources;
using System.Security.Claims;

namespace Core.Server.Common.Entities.Helpers
{
    public interface IJwtManager
    {
        string GenerateToken(UserResource user, string secret);
        UserResource GetUser(ClaimsPrincipal ClaimsPrincipal);
    }
}