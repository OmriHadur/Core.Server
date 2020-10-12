using Microsoft.AspNetCore.Mvc;
using Core.Server.Common.Errors;
using Core.Server.Shared.Errors;
using System;
using Unity;
using Core.Server.Common.Applications;
using Core.Server.Shared.Resources.Users;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Core.Server.Common.Mappers;

namespace Core.Server.Application
{
    public class BaseApplication
    {
        private UserResource currentUser;

        [Dependency]
        public IUnityContainer UnityContainer;

        [Dependency]
        public ILogger<BaseApplication> Logger;

        public UserResource CurrentUser
        {
            get
            {
                if (currentUser == null)
                    currentUser = GetCurrentUser();
                return currentUser;
            }
        }

        public virtual Func<UserResource> GetCurrentUser { get; set; }

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

        protected async Task<bool> IsEntityExists<TFEntity>(string entityId)
            where TFEntity : Entity
        {
            var repository = UnityContainer.Resolve<IQueryRepository<TFEntity>>();
            return await repository.Exists(entityId);
        }
    }
}
