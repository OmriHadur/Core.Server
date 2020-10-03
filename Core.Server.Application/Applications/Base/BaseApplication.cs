using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Core.Server.Common.Errors;
using Core.Server.Shared.Errors;
using System;
using Unity;
using Core.Server.Common.Applications;
using Core.Server.Shared.Resources.Users;
using System.Threading.Tasks;
using Core.Server.Shared.Resources;
using Core.Server.Common.Entities;
using System.Collections.Generic;
using Core.Server.Common.Repositories;

namespace Core.Server.Application
{
    public class BaseApplication: IBaseApplication
    {
        [Dependency]
        public IMapper Mapper { get; set; }

        [Dependency]
        public IUnityContainer UnityContainer { get; set; }

        public UserResource CurrentUser { get; set; }

        //[Dependency]
        //public ILogger<ApplicationBase> Logger { get; set; }

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

        protected ActionResult<TResource> Map<TResource, TEntity>(TEntity entity)
            where TResource: Resource
            where TEntity: Entity
        {
            return Ok(Mapper.Map<TResource>(entity));
        }

        protected ActionResult<IEnumerable<TResource>> MapMany<TResource, TEntity>(IEnumerable<TEntity> entities)
            where TResource : Resource
            where TEntity : Entity
        {
            return Ok(Mapper.Map<IEnumerable<TResource>>(entities));
        }

        protected async Task<bool> IsEntityExists<TFEntity>(string entityId)
            where TFEntity : Entity
        {
            var repository = UnityContainer.Resolve<IQueryRepository<TFEntity>>();
            return await repository.Exists(entityId);
        }

        protected async Task<TFEntity> GetEntity<TFEntity>(string entityId)
            where TFEntity : Entity
        {
            var repository = UnityContainer.Resolve<IQueryRepository<TFEntity>>();
            return await repository.Get(entityId);
        }
    }
}
