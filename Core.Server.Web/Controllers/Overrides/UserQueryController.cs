using Core.Server.Shared.Resources.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.Server.Web.Controllers
{
    [Authorize]
    [Route("user")]
    public class UserQueryController 
        : QueryController<UserResource>
    {
    }
}
