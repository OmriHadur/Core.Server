using Core.Server.Common.Entities.Helpers;
using Microsoft.AspNetCore.Mvc;
using Unity;
using Core.Server.Shared.Errors;
using Core.Server.Shared.Resources.Users;
using Core.Server.Common.Errors;
using Core.Server.Common.Applications;

namespace Core.Server.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ControllerBase<TApplication> 
        : ControllerBase
        where TApplication : IApplicationBase
    {
        [Dependency]
        public TApplication Application { get; set; }

        [Dependency]
        public IJwtManager JwtManager { get; set; }

        protected UserResource GetUser()
        {
            return JwtManager.GetUser(User);
        }

        protected void SetUser()
        {
            Application.CurrentUser = GetUser();
        }
    }
}
