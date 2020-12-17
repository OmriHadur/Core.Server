﻿using Core.Server.Application.Mappers.Base;
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

        public async override Task<IEnumerable<RoleResource>> Map(IEnumerable<RoleEntity> entities)
        {
            var roleResources = await base.Map(entities);
            var policiesIds = entities.SelectMany(e => e.PoliciesId).Distinct().ToArray();
            var policies = await PolicyLookupRepository.Get(policiesIds);
            var policiesResources = await PolicyMapper.Map(policies);
            foreach (var roleResource in roleResources)
            {
                var roleEntity = entities.FirstOrDefault(e => e.Id == roleResource.Id);
                roleResource.Policies = policiesResources.Where(p => roleEntity.PoliciesId.Contains(p.Id)).ToArray();
            }
            return roleResources;
        }
    }
}