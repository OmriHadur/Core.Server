using Core.Server.Application.Mappers.Base;
using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Mappers;
using Core.Server.Common.Repositories;
using Core.Server.Shared.Resources;
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
            var roleResource = await base.Map(entity);
            var rolesIds = entity.Roles.Select(r => r.Id);
            var roles = await RoleLookupRepository.Get(rolesIds.ToArray());
            roleResource.Roles = (await RoleMapper.Map(roles.ToList())).ToArray();
            return roleResource;
        }
    }
}