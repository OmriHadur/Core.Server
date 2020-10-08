using Core.Server.Common.Entities.Helpers;
using Microsoft.AspNetCore.Mvc;
using Unity;
using Core.Server.Shared.Errors;
using Core.Server.Shared.Resources.Users;
using Core.Server.Common.Errors;
using Core.Server.Common.Applications;
using Microsoft.Extensions.Logging;
using Core.Server.Shared.Resources;

namespace Core.Server.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController: ControllerBase
    {
        [Dependency]
        public IJwtManager JwtManager { get; set; }

        [Dependency]
        public IBaseApplication ApplicationBase;

        protected UserResource GetUser()
        {
            return JwtManager.GetUser(User);
        }

        protected void SetUser()
        {
            ApplicationBase.CurrentUser = GetUser();
        }
    }
}
