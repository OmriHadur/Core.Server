using Core.Server.Common.Applications;
using Core.Server.Common.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity;
using Core.Server.Shared.Resources;
using Core.Server.Shared.Query;
using Core.Server.Application.Helpers;
using Core.Server.Shared.Errors;
using Core.Server.Common.Mappers;
using Core.Server.Injection.Attributes;

namespace Core.Server.Application
{
    [Inject]
    public class QueryApplication<TResource, TEntity>
        : BaseApplication<TEntity>,
          IQueryApplication<TResource>
        where TResource : Resource
        where TEntity : Entity
    {
        [Dependency]
        public IQueryBuilder QueryBuilder;

        [Dependency]
        public IResourceMapper<TResource, TEntity> ResourceMapper;

        public virtual async Task<ActionResult<IEnumerable<TResource>>> Query(QueryResource queryResource)
        {
            var validationError = QueryBuilder.Validate<TResource>(queryResource);
            if (validationError != null)
                return BadRequest((BadRequestReason)validationError);

            var query = QueryBuilder.Build<TResource>(queryResource);
            var entities = await QueryRepository.Query(query);
            return Ok(await ResourceMapper.Map(entities));
        }
    }
}