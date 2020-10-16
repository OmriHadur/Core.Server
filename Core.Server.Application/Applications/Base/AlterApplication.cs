using Core.Server.Common.Applications;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Unity;
using Core.Server.Shared.Resources;
using Core.Server.Common.Validators;
using Core.Server.Common.Mappers;
using Core.Server.Shared.Resources.Users;
using System;
using Core.Server.Injection.Attributes;

namespace Core.Server.Application
{
    [Inject]
    public class AlterApplication<TCreateResource, TUpdateResource, TResource, TEntity>
        : BaseApplication,
          IAlterApplication<TCreateResource, TUpdateResource, TResource>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
        where TEntity : Entity, new()
    {
        [Dependency]
        public IQueryRepository<TEntity> QueryRepository { get; set; }

        [Dependency]
        public IAlterRepository<TEntity> AlterRepository { get; set; }

        [Dependency]
        public IResourceValidator<TCreateResource, TUpdateResource, TEntity> ResourceValidator { get; set; }

        [Dependency]
        public IAlterResourceMapper<TCreateResource, TUpdateResource, TResource, TEntity> ResourceMapper { get; set; }

        public override Func<UserResource> GetCurrentUser 
        { 
            set
            {
                base.GetCurrentUser = value;
                ResourceValidator.GetCurrentUser = value;
            }
        }

        public virtual async Task<ActionResult<TResource>> Create(TCreateResource resource)
        {
            var validation = await ResourceValidator.Validate(resource);
            if (!(validation is OkResult))
                return validation;
            var entity = await ResourceMapper.Map(resource);
            await AlterRepository.Add(entity);
            return await ResourceMapper.Map(entity);
        }

        public virtual async Task<ActionResult<TResource>> Replace(string id, TUpdateResource resource)
        {
            var getEntityResult = await GetEntity(id, resource);
            if (getEntityResult.Result != null)
                return getEntityResult.Result;
            var entity = getEntityResult.Value;
            await ResourceMapper.Map(resource, entity);
            await AlterRepository.Replace(entity);
            return await ResourceMapper.Map(entity);
        }

        public virtual async Task<ActionResult<TResource>> Update(string id, TUpdateResource resource)
        {
            var getEntityResult = await GetEntity(id, resource);
            if (getEntityResult.Result != null)
                return getEntityResult.Result;
            var entity = getEntityResult.Value;
            await ResourceMapper.Map(resource, entity);
            await AlterRepository.Update(entity);
            return await ResourceMapper.Map(entity);
        }

        private async Task<ActionResult<TEntity>> GetEntity(string id, TUpdateResource resource)
        {
            var entity = await QueryRepository.Get(id);
            if (entity == null)
                return NotFound(id);
            var validation = await ResourceValidator.Validate(resource, entity);
            if (!(validation is OkResult))
                return validation;
            return entity;
        }
        public virtual async Task<ActionResult> Delete(string id)
        {
            var entity = await QueryRepository.Get(id);
            if (entity == null)
                return NotFound(id);
            return await DeleteEntity(entity);
        }

        protected virtual async Task<ActionResult> DeleteEntity(TEntity entity)
        {
            await AlterRepository.Delete(entity);
            return Ok();
        }
    }
}