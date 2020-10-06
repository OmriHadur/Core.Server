using Core.Server.Common.Applications;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity;
using Core.Server.Shared.Resources;
using System.Linq;
using System;
using Core.Server.Common.Mappers;
using Core.Server.Common.Validators;

namespace Core.Server.Application
{
    public class BatchApplication<TCreateResource, TUpdateResource, TResource, TEntity>
        : BaseApplication,
        IBatchApplication<TCreateResource, TUpdateResource, TResource>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
        where TEntity : Entity, new()
    {
        [Dependency]
        public IQueryRepository<TEntity> QueryRepository;

        [Dependency]
        public IBatchRepository<TEntity> BatchRepository { get; set; }

        [Dependency]
        public IResourceValidator<TCreateResource, TUpdateResource, TEntity> ResourceValidator { get; set; }

        [Dependency]
        public IAlterResourceMapper<TCreateResource, TUpdateResource, TResource, TEntity> ResourceMapper { get; set; }

        public async Task<ActionResult<IEnumerable<TResource>>> BatchCreate(TCreateResource[] resources)
        {
            foreach (var resource in resources)
            {
                var validation = await ResourceValidator.Validate(resource);
                if (!(validation is OkResult))
                    return validation;
            }

            var entitiesTasks = resources.Select(async resource =>await ResourceMapper.Map(resource));
            var entities = entitiesTasks.Select(er => er.Result);
            await AddEntites(entities);
            return Ok(await ResourceMapper.Map(entities));
        }

        private Task AddEntites(IEnumerable<TEntity> entities)
        {
            return BatchRepository.AddMany(entities);
        }

        public async Task<ActionResult<IEnumerable<TResource>>> BatchGet(string[] ids)
        {
            var entities = await QueryRepository.FindAll(e => ids.Contains(e.Id));

            var notFoundId = ids.FirstOrDefault(id => !entities.Any(e => e.Id == id));
            if (notFoundId != null)
                return NotFound(notFoundId);

            return Ok(await ResourceMapper.Map(entities));
        }
        public Task<ActionResult<IEnumerable<TResource>>> BatchUpdate(TUpdateResource[] resources)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult> BatchDelete(string[] ids)
        {
            await BatchRepository.DeleteMany(ids);
            return Ok();
        }
    }
}