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

namespace Core.Server.Application
{
    public class BaseApplication: IBaseApplication
    {
        [Dependency]
        public IUnityContainer UnityContainer { get; set; }

        [Dependency]
        public ILogger<BaseApplication> Logger { get; set; }

        public UserResource CurrentUser { get; set; }

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

        protected async Task<bool> IsEntityExists<TFEntity>(string entityId)
            where TFEntity : Entity
        {
            var repository = UnityContainer.Resolve<IQueryRepository<TFEntity>>();
            return await repository.Exists(entityId);
        }
    }
}
