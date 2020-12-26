using Core.Server.Common.Applications;
using Core.Server.Common.Entities.Helpers;
using Core.Server.Common.Helpers;
using Core.Server.Injection.Interfaces;
using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Unity;

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
        public IReflactionHelper ReflactionHelper;

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
            var result = await AuthorizationService.AuthorizeAsync(User, GetResourceName(), operation);
            return !result.Succeeded;
        }

        private string GetResourceName()
        {
            return ReflactionHelper.GetTypeFullName(typeof(TResource));
        }
    }
}