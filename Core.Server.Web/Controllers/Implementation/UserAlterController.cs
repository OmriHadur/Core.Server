using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Core.Server.Shared.Resources.Users;
using System.Threading.Tasks;

namespace Core.Server.Web.Controllers
{
    [Authorize]
    public class UserAlterController :
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
