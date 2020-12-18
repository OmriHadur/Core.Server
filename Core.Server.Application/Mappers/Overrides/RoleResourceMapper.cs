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
    public class RoleResourceMapper
        : ResourceMapper<RoleResource, RoleEntity>
    {
        [Dependency]
        public ILookupRepository<PolicyEntity> PolicyLookupRepository;

        [Dependency]
        public IResourceMapper<PolicyResource, PolicyEntity> PolicyMapper;

        public async override Task<RoleResource> Map(RoleEntity entity)
        {
            var roleResource = await base.Map(entity);
            var policies = await PolicyLookupRepository.Get(entity.PoliciesId);
            roleResource.Policies = (await PolicyMapper.Map(policies)).ToArray();
            return roleResource;
        }

        public async override Task<IEnumerable<RoleResource>> Map(IEnumerable<RoleEntity> rolesEntities)
        {
            var roleResources = await base.Map(rolesEntities);
            var policiesIds = rolesEntities
                .SelectMany(role => role.PoliciesId)
                .Distinct()
                .ToArray();
            var policies = await PolicyLookupRepository.Get(policiesIds);
            var policiesResources = await PolicyMapper.Map(policies);
            policiesResources = policiesResources.Where(p => p != null);

            foreach (var roleResource in roleResources)
            {
                var roleEntity = rolesEntities.FirstOrDefault(e => e.Id == roleResource.Id);
                roleResource.Policies = policiesResources.Where(p => roleEntity.PoliciesId.Contains(p.Id)).ToArray();
            }
            return roleResources;
        }
    }
}