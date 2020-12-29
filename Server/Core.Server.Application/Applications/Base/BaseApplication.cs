using Core.Server.Common;
using Core.Server.Common.Applications;
using Core.Server.Common.Entities;
using Core.Server.Common.Errors;
using Core.Server.Common.Helpers;
using Core.Server.Common.Repositories;
using Core.Server.Shared.Errors;
using Core.Server.Shared.Resources.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace Core.Server.Application
{
    public class BaseApplication<TEntity>
        : IBaseApplication
        where TEntity : Entity
    {
        public UserResource CurrentUser => CurrentUserGetter.CurrentUser;

        [Dependency]
        public ICurrentUserGetter CurrentUserGetter { get; set; }

        [Dependency]
        public ILogger<BaseApplication<TEntity>> Logger;

        [Dependency]
        public ILookupRepository<TEntity> LookupRepository;

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

        public ActionResult NotFound()
        {
            return new NotFoundResult();
        }

        public ActionResult NotFound(string obj)
        {
            return new NotFoundObjectResult(obj);
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

        protected ActionResult GetValidationResult(IEnumerable<StringKeyValuePair> validationErrors)
        {
            var validationProblemDetails = new ValidationProblemDetails();
            var groupBy = validationErrors.GroupBy(kvp => kvp.Key);
            foreach (var group in groupBy)
                validationProblemDetails.Errors.Add(group.Key, group.Select(g => g.Value).ToArray());
            return new BadRequestObjectResult(validationProblemDetails);
        }

        protected ActionResult GetValidationResult(StringKeyValuePair validationError)
        {
            var validationProblemDetails = new ValidationProblemDetails();
            validationProblemDetails.Errors.Add(validationError.Key, new string[] { validationError.Value });
            return new BadRequestObjectResult(validationProblemDetails);
        }
    }
}
