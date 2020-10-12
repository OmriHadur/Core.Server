using Core.Server.Common.Entities.Helpers;
using Microsoft.AspNetCore.Mvc;
using Unity;
using Core.Server.Shared.Resources.Users;
using Core.Server.Common.Applications;

namespace Core.Server.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseController<TApplication> 
        : ControllerBase
        where TApplication: IBaseApplication
    {
        [Dependency]
        public IJwtManager JwtManager { get; set; }

        [Dependency]
        public TApplication Application;

        protected UserResource GetUser()
        {
            return JwtManager.GetUser(User);
        }

        protected void SetUser()
        {
            Application.GetCurrentUser = () => GetUser();
        }
    }
}
