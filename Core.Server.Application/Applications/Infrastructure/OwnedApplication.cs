using Core.Server.Common.Applications;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity;
using Core.Server.Shared.Resources;
using Core.Server.Common.Mappers;
using Core.Server.Injection.Attributes;

namespace Core.Server.Application
{
    [Inject]
    public class OwnedApplication<TResource, TEntity>
        : BaseApplication<TEntity>,
          IOwnedApplication<TResource>
        where TResource : Resource
        where TEntity : OwnedEntity
    {
        [Dependency]
        public IResourceMapper<TResource, TEntity> ResourceMapper;

        public virtual async Task<ActionResult<IEnumerable<TResource>>> GetAllOwned()
        {
            var entities = await LookupRepository.FindAll(e => e.UserId == CurrentUser.Id);
            return Ok(await ResourceMapper.Map(entities));
        }

        public virtual async Task<ActionResult> Any()
        {
            return await LookupRepository.Exists(e => e.UserId == CurrentUser.Id) ?
                Ok() :
                NotFound();
        }
    }
}