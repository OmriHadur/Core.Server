using Microsoft.AspNetCore.Authorization;
using Core.Server.Shared.Resources.Users;

namespace Core.Server.Web.Controllers
{
    [Authorize]
    public class UserQueryController : QueryController< UserResource>
    {
    }
}
