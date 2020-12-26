using Core.Server.Common.Applications;
using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Mappers;
using Core.Server.Common.Repositories;
using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;

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

        [Dependency]
        public IAlterRepository<TEntity> AlterRepository;

        [Dependency]
        public ILookupRepository<UserEntity> UserRepository;

        public virtual async Task<ActionResult<IEnumerable<TResource>>> GetAllOwned()
        {
            var entities = await LookupRepository.FindAll(e => e.UserId == CurrentUser.Id);
            var response = await ResourceMapper.Map(entities);
            return response.ToList();
        }

        public virtual async Task<ActionResult> Any()
        {
            return await LookupRepository.Exists(e => e.UserId == CurrentUser.Id) ?
                Ok() :
                NotFound();
        }
        public async Task<ActionResult> Assign(string resourceId)
        {
            var entity = await LookupRepository.FindFirst(e => e.Id == resourceId);
            if (entity == null)
                return NotFound(resourceId);
            if (entity.UserId != null)
                return Unauthorized();
            entity.UserId = CurrentUser.Id;
            await AlterRepository.Update(entity);
            return Ok();
        }

        public async Task<ActionResult> Reassign(ReassginResource reassginResource)
        {
            var entity = await LookupRepository.FindFirst(e => e.Id == reassginResource.ResourceId);
            if (entity == null)
                return NotFound(reassginResource.ResourceId);
            var isExists = await UserRepository.Exists(reassginResource.NewOwnerUserId);
            if (!isExists)
                return NotFound(reassginResource.NewOwnerUserId);

            entity.UserId = reassginResource.NewOwnerUserId;
            await AlterRepository.Update(entity);
            return Ok();
        }
    }
}