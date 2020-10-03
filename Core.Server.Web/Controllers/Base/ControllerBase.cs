using Core.Server.Common.Entities.Helpers;
using Microsoft.AspNetCore.Mvc;
using Unity;
using Core.Server.Shared.Errors;
using Core.Server.Shared.Resources.Users;
using Core.Server.Common.Errors;

namespace Core.Server.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        [Dependency]
        public IJwtManager JwtManager { get; set; }

        protected UserResource GetUser()
        {
            return JwtManager.GetUser(User);
        }
    }
}
