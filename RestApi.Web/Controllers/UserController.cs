using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RestApi.Shared.Resources.Users;
using System.Threading.Tasks;

namespace RestApi.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : RestController<UserCreateResource, UserResource>
    {
        [HttpPost]
        [AllowAnonymous]
        public override async Task<ActionResult<UserResource>> Create(UserCreateResource resource)
        {
            return await Application.Create(resource);
        }
    }
}
