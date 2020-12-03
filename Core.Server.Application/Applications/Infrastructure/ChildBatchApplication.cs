using Core.Server.Common.Applications;
using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Mappers;
using Core.Server.Common.Repositories;
using Core.Server.Common.Validators;
using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application
{
    [Inject]
    public class ChildBatchApplication<TCreateResource, TUpdateResource, TResource, TParentEntity, TChildEntity>
        : BaseApplication<TParentEntity>,
          IChildBatchApplication<TCreateResource, TUpdateResource, TResource>
        where TCreateResource : ChildCreateResource
        where TUpdateResource : ChildUpdateResource
        where TResource : Resource
        where TParentEntity : Entity
        where TChildEntity : Entity
    {
        [Dependency]
        public IBatchRepository<TParentEntity> BatchRepository;

        [Dependency]
        public IResourceValidator<TCreateResource, TUpdateResource, TParentEntity> ResourceValidator;

        [Dependency]
        public IAlterResourceMapper<TCreateResource, TUpdateResource, TChildEntity> AlterResourceMapper;

        [Dependency]
        public IResourceMapper<TResource, TParentEntity> ResourceMapper;

        [Dependency]
        public IParentManager<TParentEntity, TChildEntity> ParentManager;

        public async Task<ActionResult<IEnumerable<TResource>>> BatchCreate(TCreateResource[] resources)
        {
            var parentsIds = resources.Select(r => r.ParentId).Distinct();
            var result = await GetParents(parentsIds);
            if (!(result is OkResult))
                return result.Result;
            var parents = result.Value;

            var validationResult = await Validate(resources, parents);
            if (IsNotOk(validationResult))
                return validationResult;

            AddChildren(resources, parents);

            await BatchRepository.UpdateMany(parents.Values);
            var response = await ResourceMapper.Map(parents.Values);
            return response.ToList();
        }

        public Task<ActionResult<IEnumerable<TResource>>> BatchUpdate(TUpdateResource[] resources)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<string>>> BatchDelete(ChildBatchDeleteResource childBatchDeleteResource)
        {
            var parentsIds = childBatchDeleteResource.ParentAndIds.Keys.ToList();
            var result = await GetParents(parentsIds);
            if (!(result is OkResult))
                return result.Result;
            var parents = result.Value;

            foreach (var parentId in parentsIds)
            {
                var childrenIds = childBatchDeleteResource.ParentAndIds[parentId];
                foreach (var childId in childrenIds)
                {
                    if (!ParentManager.Exists(parents[parentId], childId))
                        return NotFound(childId);
                    ParentManager.Remove(parents[parentId], childId);
                }
                
            }

            await BatchRepository.UpdateMany(parents.Values);
            return Ok(parentsIds);
        }

        private void AddChildren(TCreateResource[] resources, Dictionary<string, TParentEntity> parents)
        {
            foreach (var parentKeyValue in parents)
            {
                var resourcesForParent = resources.Where(r => r.ParentId == parentKeyValue.Key);
                var entitiesTasks = resourcesForParent.Select(async r => await AlterResourceMapper.Map(r));
                var entities = entitiesTasks.Select(er => er.Result).ToList();
                ParentManager.Add(parentKeyValue.Value, entities);
            }
        }

        private async Task<ActionResult<Dictionary<string, TParentEntity>>> GetParents(IEnumerable<string> parentsIds)
        {
            var parents = new Dictionary<string, TParentEntity>();
            foreach (var parentsId in parentsIds)
            {
                var parnet = await LookupRepository.Get(parentsId);
                if (parnet == null)
                    return NotFound(parentsId);
                parents.Add(parentsId, parnet);
            }
            return parents;
        }
        private async Task<ActionResult> Validate(TCreateResource[] resources, Dictionary<string, TParentEntity> parents)
        {
            foreach (var resource in resources)
            {
                var validationResult = await ResourceValidator.Validate(resource, parents[resource.ParentId]);
                if (IsNotOk(validationResult))
                    return validationResult;
            }
            return Ok();
        }
    }
}