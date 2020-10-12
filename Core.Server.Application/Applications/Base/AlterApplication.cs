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
using Core.Server.Common.Attributes;

namespace Core.Server.Application
{
    [InjectBoundle]
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

        public virtual async Task<ActionResult<TResource>> CreateOrUpdate(string id, TCreateResource resource)
        {
            var entity = await QueryRepository.Get(id);
            var validation = await Validate(resource, entity);
            if (!(validation is OkResult))
                return validation;
            await Map(resource, entity);
            await AlterRepository.Update(entity);
            return await ResourceMapper.Map(entity);
        }

        public virtual async Task<ActionResult<TResource>> Update(string id, TUpdateResource resource)
        {
            var entity = await QueryRepository.Get(id);
            if (entity == null)
                return NotFound(id);
            var validation = await ResourceValidator.Validate(resource, entity);
            if (!(validation is OkResult))
                return validation;
            await ResourceMapper.Map(resource, entity);
            await AlterRepository.Update(entity);
            return await ResourceMapper.Map(entity);
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

        private async Task Map(TCreateResource resource, TEntity entity)
        {
            if (entity == null)
                await ResourceMapper.Map(resource);
            else
                 await ResourceMapper.Map(resource, entity);
        }

        private async Task<ActionResult> Validate(TCreateResource resource, TEntity entity)
        {
            return entity == null
             ? await ResourceValidator.Validate(resource)
             : await ResourceValidator.Validate(resource, entity);
        }
    }
}