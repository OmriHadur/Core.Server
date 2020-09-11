using RestApi.Common.Entities.Helpers;
using Microsoft.AspNetCore.Mvc;
using Unity;
using RestApi.Standard.Shared.Errors;
using RestApi.Standard.Shared.Resources.Users;
using RestApi.Common.Errors;

namespace RestApi.Web.Controllers
{
    public class Controller : ControllerBase
    {
        [Dependency]
        public IJwtManager JwtManager { get; set; }

        protected ActionResult BadRequest(BadRequestReason badRequestReason)
        {
            return new BadRequestApplicationResult(badRequestReason);
        }

        protected UserResource GetUser()
        {
            return JwtManager.GetUser(User);
        }
    }
}
