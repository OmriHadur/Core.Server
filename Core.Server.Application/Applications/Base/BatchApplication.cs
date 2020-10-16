using Core.Server.Common.Applications;
using Core.Server.Injection.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Mappers;
using Core.Server.Common.Repositories;
using Core.Server.Common.Validators;
using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application
{
    [Inject]
    public class BatchApplication<TCreateResource, TUpdateResource, TResource, TEntity>
        : BaseApplication,
          IBatchApplication<TCreateResource, TUpdateResource, TResource>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
        where TEntity : Entity, new()
    {
        [Dependency]
        public IQueryRepository<TEntity> QueryRepository { get; set; }

        [Dependency]
        public IBatchRepository<TEntity> BatchRepository;

        [Dependency]
        public IResourceValidator<TCreateResource, TUpdateResource, TEntity> ResourceValidator;

        [Dependency]
        public IAlterResourceMapper<TCreateResource, TUpdateResource, TResource, TEntity> ResourceMapper;

        public async Task<ActionResult<IEnumerable<TResource>>> BatchCreate(TCreateResource[] resources)
        {
            var validationResult =await Validate(resources);
            if (IsNotOk(validationResult))
                return validationResult;
            var entitiesTasks = resources.Select(async resource =>await ResourceMapper.Map(resource));
            var entities = entitiesTasks.Select(er => er.Result);
            await AddEntites(entities);
            return Ok(await ResourceMapper.Map(entities));
        }

        private async Task<ActionResult> Validate(TCreateResource[] resources)
        {
            foreach (var resource in resources)
            {
                var validationResult = await ResourceValidator.Validate(resource);
                if (IsNotOk(validationResult))
                    return validationResult;
            }
            return Ok();
        }

        private Task AddEntites(IEnumerable<TEntity> entities)
        {
            return BatchRepository.AddMany(entities);
        }

        public Task<ActionResult<IEnumerable<TResource>>> BatchUpdate(TUpdateResource[] resources)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<string>>> BatchDelete(string[] ids)
        {
            await BatchRepository.DeleteMany(ids);
            return Ok(ids);
        }
    }
}