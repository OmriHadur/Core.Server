using Core.Server.Common.Applications;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Unity;
using Core.Server.Shared.Resources;
using Core.Server.Common.Validators;
using Core.Server.Common.Mappers;
using Core.Server.Injection.Attributes;
using Core.Server.Shared.Resources.Users;

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
        public IAlterCachedRepository<TEntity> AlterRepository;

        [Dependency]
        public IResourceValidator<TCreateResource, TUpdateResource, TEntity> ResourceValidator;

        [Dependency]
        public IAlterResourceMapper<TCreateResource, TUpdateResource, TEntity> AlterResourceMapper;

        [Dependency]
        public IResourceMapper<TResource, TEntity> ResourceMapper;

        public override UserResource CurrentUser
        {
            get => base.CurrentUser;
            set
            {
                base.CurrentUser = value;
                ResourceValidator.CurrentUser = value;
            }
        }

        public virtual async Task<ActionResult<TResource>> Create(TCreateResource resource)
        {
            var validation = await ResourceValidator.Validate(resource);
            if (!(validation is OkResult))
                return validation;
            var entity = await AlterResourceMapper.Map(resource);
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
            var entity = await LookupRepository.Get(id);
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