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
        where TApplication : IBaseApplication
    {
        private TApplication application;

        [Dependency]
        public IJwtManager JwtManager { get; set; }

        [Dependency]
        public TApplication Application
        {
            get => application;
            set
            {
                application = value;
                SetUser(value);
            }
        }

        protected UserResource GetUser()
        {
            return JwtManager.GetUser(User);
        }

        protected void SetUser(IBaseApplication application)
        {
            application.GetCurrentUser = () => GetUser();
        }
    }
}
