using Core.Server.Application.Mappers.Base;
using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Core.Server.Shared.Resources;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application.Mappers.Implementation
{
    [Inject]
    public class RoleAlterResourceMapper
        : ResourceMapper<RoleResource,RoleEntity>
    {
        [Dependency]
        public ILookupRepository<PolicyEntity> PolicyLookupRepository;

        [Dependency]
        public ResourceMapper<PolicyResource, PolicyEntity> PolicyMapper;

        public async override Task<RoleResource> Map(RoleEntity entity)
        {
            var roleResource = await base.Map(entity);
            var policies = await PolicyLookupRepository.Get(entity.PoliciesId);
            roleResource.Policies = (await PolicyMapper.Map(policies)).ToArray();
            return roleResource;
        }
    }
}
