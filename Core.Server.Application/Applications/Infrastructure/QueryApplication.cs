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
using Core.Server.Common.Query;
using Core.Server.Shared.Query;
using Core.Server.Common.Query.Infrastructure;

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
        public IQueryValidator QueryValidator;

        [Dependency]
        public IQueryResourceValidator QueryResourceValidator;

        [Dependency]
        public IResourceMapper<TResource, TEntity> ResourceMapper;

        [Dependency]
        public IQueryResourceMapper QueryResourceMapper;

        public virtual async Task<ActionResult<IEnumerable<TResource>>> Query(QueryResource queryResource)
        {
            var queryRequest = QueryResourceMapper.Map<TResource>(queryResource);

            var validateResult = Validate(queryRequest);
            if (validateResult != null)
                return BadRequest((BadRequestReason)validateResult);

            var entities = await QueryRepository.Query(queryRequest);
            return Ok(await ResourceMapper.Map(entities));
        }

        private BadRequestReason? Validate(QueryRequest queryRequest)
        {
            var queryResourceValidation = QueryResourceValidator.Validate<TResource>(queryRequest);
            var queryPhraseValidation = QueryValidator.Validate<TResource>(queryRequest.Query);
            return queryResourceValidation ?? queryPhraseValidation;
        }
    }
}