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
        public IUnityContainer UnityContainer { get; set; }

        [Dependency]
        public TApplication Application
        {
            get
            {
                SetUser();
                return application;
            }
            set
            {
                application = value;
            }
        }

        protected void SetUser()
        {
            var user = JwtManager.GetUser(User);
            application.CurrentUser = user;
        }
    }
}
