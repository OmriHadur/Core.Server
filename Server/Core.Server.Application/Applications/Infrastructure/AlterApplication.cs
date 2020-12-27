using Core.Server.Common.Applications;
using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Mappers;
using Core.Server.Common.Repositories;
using Core.Server.Common.Validators;
using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application
{
    [Inject]
    public class AlterApplication<TAlterResource, TResource, TEntity>
        : BaseApplication<TEntity>,
          IAlterApplication<TAlterResource, TResource>
        where TResource : Resource
        where TEntity : Entity
    {
        [Dependency]
        public IAlterRepository<TEntity> AlterRepository;

        [Dependency]
        public IResourceValidator<TAlterResource, TEntity> ResourceValidator;

        [Dependency]
        public IAlterResourceMapper<TAlterResource, TEntity> AlterResourceMapper;

        [Dependency]
        public IResourceMapper<TResource, TEntity> ResourceMapper;

        public virtual async Task<ActionResult<TResource>> Create(TAlterResource resource)
        {
            var validation = await ResourceValidator.ValidateCreate(resource);
            if (validation.Any())
                return GetValidationResult(validation);
            var entity = await AlterResourceMapper.MapCreate(resource);
            if (entity is OwnedEntity)
                (entity as OwnedEntity).UserId = CurrentUser.Id;
            await AlterRepository.Add(entity);
            return await ResourceMapper.Map(entity);
        }

        public virtual async Task<ActionResult<TResource>> Replace(string id, TAlterResource resource)
        {
            var entity = await LookupRepository.Get(id);
            if (entity == null)
                entity = await AlterResourceMapper.MapCreate(resource);
            var validation = await ResourceValidator.ValidateUpdate(resource, entity);
            if (validation.Any())
                return GetValidationResult(validation);
            await AlterResourceMapper.MapCreate(resource, entity);
            await AlterRepository.Replace(entity);
            return await ResourceMapper.Map(entity);
        }

        public virtual async Task<ActionResult<TResource>> Update(string id, TAlterResource resource)
        {
            var entity = await LookupRepository.Get(id);
            if (entity == null)
                return NotFound(id);
            var validation = await ResourceValidator.ValidateUpdate(resource, entity);
            if (validation.Any())
                return GetValidationResult(validation);
            await AlterResourceMapper.MapUpdate(resource, entity);
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