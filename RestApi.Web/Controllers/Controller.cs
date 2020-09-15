using RestApi.Common.Entities.Helpers;
using Microsoft.AspNetCore.Mvc;
using Unity;
using RestApi.Shared.Errors;
using RestApi.Shared.Resources.Users;
using RestApi.Common.Errors;

namespace RestApi.Web.Controllers
{
    public class Controller : ControllerBase
    {
        [Dependency]
        public IJwtManager JwtManager { get; set; }

        protected UserResource GetUser()
        {
            return JwtManager.GetUser(User);
        }
    }
}
