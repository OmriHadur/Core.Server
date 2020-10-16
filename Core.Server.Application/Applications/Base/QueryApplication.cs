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
using System.Linq;
using Core.Server.Injection.Attributes;

namespace Core.Server.Application
{
    [Inject]
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

        public virtual async Task<ActionResult<IEnumerable<TResource>>> GetAll()
        {
            var entities = await QueryRepository.Get();
            return Ok(await ResourceMapper.Map(entities));
        }

        public virtual async Task<ActionResult<IEnumerable<TResource>>> GetByIds(string[] ids)
        {
            var entities = await QueryRepository.Get(ids);
            var notFoundId = ids.FirstOrDefault(id => !entities.Any(e => e.Id == id));
            if (notFoundId != null)
                return NotFound(notFoundId);
            return Ok(await ResourceMapper.Map(entities));
        }

        public virtual async Task<ActionResult<TResource>> GetById(string id)
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