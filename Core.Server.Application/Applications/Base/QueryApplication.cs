using Core.Server.Common.Applications;
using Core.Server.Common.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity;
using Core.Server.Shared.Resources;
using Core.Server.Shared.Errors;
using Core.Server.Common.Mappers;
using Core.Server.Injection.Attributes;
using Core.Server.Application.Query;
using Core.Server.Common.Query;
using Core.Server.Shared.Query;

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
        public IQueryPhraseMapper QueryResourceToEntityMapper;

        [Dependency]
        public IQueryBaseValidator QueringValidator;

        [Dependency]
        public IResourceMapper<TResource, TEntity> ResourceMapper;

        public virtual async Task<ActionResult<IEnumerable<TResource>>> Query(QueryResource queryResource)
        {
            var query = QueryResourceToEntityMapper.Map<TResource>(queryResource.QueryPhrase);
            var validationError = QueringValidator.Validate<TResource>(query);
            if (validationError != null)
                return BadRequest((BadRequestReason)validationError);

            var entities = await QueryRepository.Query(query);
            return Ok(await ResourceMapper.Map(entities));
        }
    }
}