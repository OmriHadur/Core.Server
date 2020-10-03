using Core.Server.Common.Applications;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Unity;
using Core.Server.Shared.Resources;

namespace Core.Server.Application
{
    public class RestApplication<TCreateResource, TUpdateResource, TResource, TEntity>
        : QueryApplication<TResource, TEntity>, 
        IRestApplication<TCreateResource, TUpdateResource, TResource>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
        where TEntity : Entity, new()
    {
        [Dependency]
        public IRestRepository<TEntity> RestRepository { get; set; }

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
            var entity = await RestRepository.Get(updateResource.Id);
            if (entity == null)
                return NotFound(updateResource.Id);
            var validation = await Validate(updateResource, entity);
            if (!(validation is OkResult))
                return validation;
            Mapper.Map(updateResource, entity);
            await UpdateEntity(entity);
            entity = await RestRepository.Get(entity.Id);
            return await Map(entity);
        }

        public virtual async Task<ActionResult> Delete(string id)
        {
            var entity = await RestRepository.Get(id);
            if (entity == null)
                return NotFound(id);
            return await DeleteEntity(entity);
        }

        protected virtual async Task<ActionResult> Validate(TCreateResource createResource)
        {
            return Ok();
        }

        protected virtual async Task<ActionResult> Validate(TUpdateResource updateResource, TEntity entity)
        {
            return Ok();
        }

        protected virtual TEntity GetNewTEntity(TCreateResource resource)
        {
            return Mapper.Map<TEntity>(resource);
        }

        protected virtual async Task UpdateEntity(TEntity entity)
        {
            await RestRepository.Update(entity);
        }

        protected async virtual Task<ActionResult> DeleteEntity(TEntity entity)
        {
            await RestRepository.Delete(entity);
            return Ok();
        }

        protected virtual async Task AddEntity(TEntity entity)
        {
            await RestRepository.Add(entity);
        }
    }
}