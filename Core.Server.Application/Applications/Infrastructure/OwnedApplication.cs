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

        public async Task<ActionResult> Reassign(ReassginResource reassginResource)
        {
            var entity = await LookupRepository.FindFirst(e => e.Id == reassginResource.ResourceId);
            if (entity == null)
                return NotFound(reassginResource.ResourceId);
            if (entity.UserId != null && entity.UserId != CurrentUser.Id)
                return Unauthorized();
            var userEntity = await UserRepository.FindFirst(u => u.Email == reassginResource.UserEmail);
            if (userEntity == null)
                return NotFound(reassginResource.UserEmail);

            entity.UserId = userEntity.Id;
            await AlterRepository.Update(entity);
            return Ok();
        }
    }
}