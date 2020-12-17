using Core.Server.Common.Entities.Helpers;
using Microsoft.AspNetCore.Mvc;
using Unity;
using Core.Server.Common.Applications;
using Core.Server.Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Core.Server.Shared.Resources;

namespace Core.Server.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseController<TApplication, TResource>
        : ControllerBase
        where TApplication : IBaseApplication
        where TResource : Resource
    {
        private TApplication application;

        [Dependency]
        public IJwtManager JwtManager;

        [Dependency]
        public ICurrentUserGetter CurrentUserGetter;

        [Dependency]
        public IAuthorizationService AuthorizationService;

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

        protected async Task<bool> IsUnauthorized(OperationAuthorizationRequirement operation)
        {
            var result = await AuthorizationService.AuthorizeAsync(User, typeof(TResource), operation);
            return !result.Succeeded;
        }
    }
}