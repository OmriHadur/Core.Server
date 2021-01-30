using Core.Server.Application.Mappers.Base;
using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Mappers;
using Core.Server.Common.Repositories;
using Core.Server.Shared.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application.Mappers.Implementation
{
    [Inject]
    public class UserResourceMapper
        : ResourceMapper<UserResource, UserEntity>
    {
        [Dependency]
        public ILookupRepository<RoleEntity> RoleLookupRepository;

        [Dependency]
        public IResourceMapper<RoleResource, RoleEntity> RoleMapper;

        public async override Task<UserResource> Map(UserEntity entity)
        {
            var userResource = await base.Map(entity);
            var rolesIds = entity.Roles.Select(r => r.Id);
            var rolesEntities = await RoleLookupRepository.Get(rolesIds.ToArray());
            var rolesResources = await RoleMapper.Map(rolesEntities.ToList());
            var userRolesResources = Mapper.Map<IEnumerable<UserRoleResource>>(rolesResources);
            userResource.Roles = userRolesResources.ToArray();
            return userResource;
        }
    }
}