using Core.Server.Common.Entities.Helpers;
using Microsoft.AspNetCore.Mvc;
using Unity;
using Core.Server.Shared.Resources.Users;
using Core.Server.Common.Applications;
using Unity.Lifetime;
using System.Linq;

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
            foreach (var registration in UnityContainer.Registrations.Where(p => p.RegisteredType == typeof(UserResource)))
            {
                registration.LifetimeManager.RemoveValue();
            }

            var user = JwtManager.GetUser(User);
            UnityContainer.RegisterInstance(user);
        }

    }
}
