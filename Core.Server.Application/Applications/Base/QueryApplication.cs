using Core.Server.Common.Applications;
using Core.Server.Common.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity;
using Core.Server.Shared.Resources;
using Core.Server.Shared.Query;
using Core.Server.Shared.Errors;
using Core.Server.Common.Mappers;
using Core.Server.Injection.Attributes;
using Core.Server.Application.Query;

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
        public IQueryResourceToEntityMapper QueryResourceToEntityMapper;

        [Dependency]
        public IQueryBaseValidator QueringValidator;

        [Dependency]
        public IResourceMapper<TResource, TEntity> ResourceMapper;

        public virtual async Task<ActionResult<IEnumerable<TResource>>> Query(QueryPropertyResource queryResource)
        {

            var validationError = QueringValidator.Validate<TResource>(queryResource);
            if (validationError != null)
                return BadRequest((BadRequestReason)validationError);

            var query = QueryResourceToEntityMapper.Map<TResource>(queryResource);
            var entities = await QueryRepository.Query(query);
            return Ok((object)await ResourceMapper.Map((IEnumerable<TEntity>)entities));
        }
    }
}