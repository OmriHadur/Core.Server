using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Core.Server.Shared.Resources.Users;
using System.Threading.Tasks;
using Core.Server.Common.Applications;

namespace Core.Server.Web.Controllers
{
    [Authorize]
    public class UserController :
        AlterController<UserCreateResource, UserUpdateResource, UserResource>
    {
        [HttpPost]
        [AllowAnonymous]
        public override async Task<ActionResult<UserResource>> Create(UserCreateResource resource)
        {
            return await Application.Create(resource);
        }
    }
}
