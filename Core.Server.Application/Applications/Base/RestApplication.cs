using Core.Server.Common.Applications;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity;
using Core.Server.Shared.Resources;
using Core.Server.Shared.Resources.Users;
using Core.Server.Shared.Query;
using Core.Server.Application.Helpers;
using Core.Server.Shared.Errors;

namespace Core.Server.Application
{
    public class RestApplication<TCreateResource, TUpdateResource, TResource, TEntity>
        : ApplicationBase, IRestApplication<TCreateResource, TUpdateResource, TResource>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
        where TEntity : Entity, new()
    {
        [Dependency]
        public IRepository<TEntity> Repository { get; set; }

        [Dependency]
        public IUnityContainer UnityContainer { get; set; }

        [Dependency]
        public IQueryBuilder QueryBuilder;

        public UserResource CurrentUser { get; set; }
        public virtual async Task<ActionResult<IEnumerable<TResource>>> Get()
        {
            var entities = await Repository.Get();
            var resources = Mapper.Map<IEnumerable<TResource>>(entities);
            return Ok(resources);
        }

        public virtual async Task<ActionResult<TResource>> Get(string id)
        {
            var entity = await Repository.Get(id);
            if (entity == null)
                return NotFound(id);
            return await Map(entity);
        }

        public virtual async Task<ActionResult<IEnumerable<TResource>>> Query(QueryResource queryResource)
        {
            var validationError = QueryBuilder.Validate<TResource>(queryResource);
            if (validationError != null)
                return BadRequest((BadRequestReason)validationError);

            var query = QueryBuilder.Build<TResource>(queryResource);
            var entities = await Repository.Query(query);
            var resources = Mapper.Map<IEnumerable<TResource>>(entities);
            return Ok(resources);
        }

        public virtual async Task<ActionResult<TResource>> Create(TCreateResource createResource)
        {
            var validation = await Validate(createResource);
            if (!(validation is OkResult))
                return validation;
            var entity = GetNewTEntity(createResource);
            await AddEntity(entity);
            return await Map(entity);
        }

        public virtual async Task<ActionResult<TResource>> Update(TUpdateResource updateResource)
        {
            var entity = await Repository.Get(updateResource.Id);
            if (entity == null)
                return NotFound(updateResource.Id);
            var validation = await Validate(updateResource, entity);
            if (!(validation is OkResult))
                return validation;
            Mapper.Map(updateResource, entity);
            await UpdateEntity(entity);
            entity = await Repository.Get(entity.Id);
            return await Map(entity);
        }

        public virtual async Task<ActionResult> Delete(string id)
        {
            var entity = await Repository.Get(id);
            if (entity == null)
                return NotFound(id);
            return await DeleteEntity(entity);
        }

        public virtual async Task<ActionResult> Exists(string id)
        {
            return await Repository.IsExists(id) ?
                Ok() :
                NotFound(id);
        }

        protected virtual async Task<ActionResult> Validate(TCreateResource createResource)
        {
            return Ok();
        }

        protected virtual async Task<ActionResult> Validate(TUpdateResource updateResource, TEntity entity)
        {
            return Ok();
        }

        protected async virtual Task<TResource> Map(TEntity entity)
        {
            return Mapper.Map<TResource>(entity);
        }

        protected ActionResult<IEnumerable<TResource>> MapMany(IEnumerable<TEntity> entities)
        {
            return Ok(Mapper.Map<IEnumerable<TResource>>(entities));
        }

        protected virtual TEntity GetNewTEntity(TCreateResource resource)
        {
            return Mapper.Map<TEntity>(resource);
        }

        protected virtual async Task UpdateEntity(TEntity entity)
        {
            await Repository.Update(entity);
        }

        protected async virtual Task<ActionResult> DeleteEntity(TEntity entity)
        {
            await Repository.Delete(entity);
            return Ok();
        }

        protected virtual async Task AddEntity(TEntity entity)
        {
            await Repository.Add(entity);
        }

        protected async Task<bool> IsEntityExists<TFEntity>(string entityId)
            where TFEntity : Entity
        {
            var repository = UnityContainer.Resolve<IRepository<TFEntity>>();
            return await repository.IsExists(entityId);
        }

        protected async Task<TFEntity> GetEntity<TFEntity>(string entityId)
            where TFEntity : Entity
        {
            var repository = UnityContainer.Resolve<IRepository<TFEntity>>();
            return await repository.Get(entityId);
        }
    }
}