using Core.Server.Common.Applications;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity;
using Core.Server.Shared.Resources;
using Core.Server.Shared.Query;
using Core.Server.Application.Helpers;
using Core.Server.Shared.Errors;
using Core.Server.Common.Mappers;

namespace Core.Server.Application
{
    public class QueryApplication<TResource, TEntity>
        : BaseApplication,
        IQueryApplication<TResource>
        where TResource : Resource
        where TEntity : Entity, new()
    {
        [Dependency]
        public IQueryRepository<TEntity> QueryRepository { get; set; }

        [Dependency]
        public IQueryBuilder QueryBuilder;

        [Dependency]
        public IResourceMapper<TResource, TEntity> ResourceMapper;

        public virtual async Task<ActionResult<IEnumerable<TResource>>> Get()
        {
            var entities = await QueryRepository.Get();
            return Ok(await ResourceMapper.Map(entities));
        }

        public virtual async Task<ActionResult<TResource>> Get(string id)
        {
            var entity = await QueryRepository.Get(id);
            if (entity == null)
                return NotFound(id);
            return await ResourceMapper.Map(entity);
        }

        public virtual async Task<ActionResult<IEnumerable<TResource>>> Query(QueryResource queryResource)
        {
            var validationError = QueryBuilder.Validate<TResource>(queryResource);
            if (validationError != null)
                return BadRequest((BadRequestReason)validationError);

            var query = QueryBuilder.Build<TResource>(queryResource);
            var entities = await QueryRepository.Query(query);
            return Ok(await ResourceMapper.Map(entities));
        }

        public virtual async Task<ActionResult> Exists(string id)
        {
            return await QueryRepository.Exists(id) ?
                Ok() :
                NotFound(id);
        }
    }
}