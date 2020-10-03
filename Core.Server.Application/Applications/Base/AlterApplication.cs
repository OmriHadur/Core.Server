using Core.Server.Common.Applications;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Unity;
using Core.Server.Shared.Resources;
using Core.Server.Common.Validators;
using Core.Server.Common.Mappers;

namespace Core.Server.Application
{
    public class AlterApplication<TCreateResource, TUpdateResource, TResource, TEntity>
        : BaseApplication,
        IAlterApplication<TCreateResource, TUpdateResource, TResource>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
        where TEntity : Entity, new()
    {
        [Dependency]
        public IAlterRepository<TEntity> AlterRepository { get; set; }

        [Dependency]
        public IQueryRepository<TEntity> QueryRepository { get; set; }

        [Dependency]
        public IResourceValidator<TCreateResource, TUpdateResource, TEntity> ResourceValidator{get;set;}

        [Dependency]
        public IAlterResourceMapper<TCreateResource, TUpdateResource,TResource, TEntity> ResourceMapper { get; set; }

        public virtual async Task<ActionResult<TResource>> Create(TCreateResource createResource)
        {
            var validation = await ResourceValidator.Validate(createResource);
            if (!(validation is OkResult))
                return validation;
            var entityResult = await ResourceMapper.Map(createResource);
            if (entityResult.Result != null)
                return entityResult.Result;
            await AlterRepository.Add(entityResult.Value);
            return await ResourceMapper.Map(entityResult.Value);
        }

        public virtual async Task<ActionResult<TResource>> Update(TUpdateResource updateResource)
        {
            var entity = await QueryRepository.Get(updateResource.Id);
            if (entity == null)
                return NotFound(updateResource.Id);
            var validation = await ResourceValidator.Validate(updateResource, entity);
            if (!(validation is OkResult))
                return validation;
            Mapper.Map(updateResource, entity);
            await AlterRepository.Update(entity);
            entity = await QueryRepository.Get(entity.Id);
            return await ResourceMapper.Map(entity);
        }

        public virtual async Task<ActionResult> Delete(string id)
        {
            var entity = await QueryRepository.Get(id);
            if (entity == null)
                return NotFound(id);
            await AlterRepository.Delete(entity);
            return Ok();
        }
    }
}