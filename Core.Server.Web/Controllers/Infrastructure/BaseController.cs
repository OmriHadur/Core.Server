using Core.Server.Common.Entities.Helpers;
using Microsoft.AspNetCore.Mvc;
using Unity;
using Core.Server.Common.Applications;
using Core.Server.Common.Helpers;

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
        public IJwtManager JwtManager;

        [Dependency]
        public ICurrentUserGetter CurrentUserGetter;

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
            CurrentUserGetter.CurrentUser = JwtManager.GetUser(User);
        }

    }
}