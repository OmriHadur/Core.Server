using Microsoft.AspNetCore.Mvc;
using Core.Server.Common.Errors;
using Core.Server.Shared.Errors;
using System;
using Unity;
using Core.Server.Shared.Resources.Users;
using Microsoft.Extensions.Logging;
using Core.Server.Common.Applications;

namespace Core.Server.Application
{
    public class BaseApplication
        : IBaseApplication
    {
        public virtual UserResource CurrentUser { get; set; }

        [Dependency]
        public ILogger<BaseApplication> Logger { get; set; }

        protected ActionResult BadRequest(BadRequestReason badRequestReason)
        {
            return BadRequest<BadRequestReason>(badRequestReason);
        }

        protected ActionResult BadRequest<TReson>(TReson badRequestReason)
            where TReson : struct, Enum
        {
            return new BadRequestApplicationResult(
                Convert.ToInt32(badRequestReason), 
                Enum.GetName(typeof(TReson), badRequestReason));
        }

        public ActionResult NotFound(string obj)
        {
            return new NotFoundApplicationResult(obj);
        }

        public ActionResult Ok()
        {
            return new OkResult();
        }

        public ActionResult Ok(object obj)
        {
            return new OkObjectResult(obj);
        }

        public ActionResult Unauthorized()
        {
            return new UnauthorizedResult();
        }

        protected bool IsOk(ActionResult actionResult)
        {
            return actionResult is OkResult;
        }

        protected bool IsNotOk(ActionResult actionResult)
        {
            return !IsOk(actionResult);
        }
    }
}
