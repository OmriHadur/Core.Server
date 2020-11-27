using Core.Server.Common.Applications;
using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Mappers;
using Core.Server.Common.Repositories;
using Core.Server.Common.Validators;
using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application
{
    [Inject]
    public class AlterApplication<TCreateResource, TUpdateResource, TResource, TEntity>
        : BaseApplication<TEntity>,
          IAlterApplication<TCreateResource, TUpdateResource, TResource>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
        where TEntity : Entity
    {
        [Dependency]
        public IAlterRepository<TEntity> AlterRepository;

        [Dependency]
        public IResourceValidator<TCreateResource, TUpdateResource, TEntity> ResourceValidator;

        [Dependency]
        public IAlterResourceMapper<TCreateResource, TUpdateResource, TEntity> AlterResourceMapper;

        [Dependency]
        public IResourceMapper<TResource, TEntity> ResourceMapper;

        public virtual async Task<ActionResult<TResource>> Create(TCreateResource resource)
        {
            var validation = await ResourceValidator.Validate(resource);
            if (!(validation is OkResult))
                return validation;
            var entity = await AlterResourceMapper.Map(resource);
            if (entity is OwnedEntity)
                (entity as OwnedEntity).UserId = CurrentUser.Id;
            await AlterRepository.Add(entity);
            return await ResourceMapper.Map(entity);
        }

        public virtual async Task<ActionResult<TResource>> Replace(string id, TCreateResource resource)
        {
            var entity = await LookupRepository.Get(id);
            if (entity == null)
                entity = await AlterResourceMapper.Map(resource);
            var validation = await ResourceValidator.Validate(resource, entity);
            if (!(validation is OkResult))
                return validation;
            await AlterResourceMapper.Map(resource, entity);
            await AlterRepository.Replace(entity);
            return await ResourceMapper.Map(entity);
        }

        public virtual async Task<ActionResult<TResource>> Update(string id, TUpdateResource resource)
        {
            var entity = await LookupRepository.Get(id);
            if (entity == null)
                return NotFound(id);
            var validation = await ResourceValidator.Validate(resource, entity);
            if (!(validation is OkResult))
                return validation;
            await AlterResourceMapper.Map(resource, entity);
            await AlterRepository.Update(entity);
            return await ResourceMapper.Map(entity);
        }

        public virtual async Task<ActionResult> Delete(string id)
        {
            var exists = await LookupRepository.Exists(id);
            if (!exists)
                return NotFound(id);
            await DeleteEntity(id);
            return Ok();
        }

        public async virtual Task<ActionResult> DeleteAll()
        {
            await AlterRepository.DeleteAll();
            return Ok();
        }

        protected virtual Task DeleteEntity(string id)
        {
            return AlterRepository.Delete(id);
        }
    }
}