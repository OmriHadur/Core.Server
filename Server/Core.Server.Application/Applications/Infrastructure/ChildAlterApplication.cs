using Core.Server.Common.Applications;
using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Mappers;
using Core.Server.Common.Repositories;
using Core.Server.Common.Validators;
using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application
{
    [Inject]
    public class ChildAlterApplication<TAlterResource, TParentResource, TParentEntity, TChildEntity>
        : BaseApplication<TParentEntity>,
          IChildAlterApplication<TAlterResource, TParentResource>
        where TAlterResource : ChildAlterResource
        where TParentResource : Resource
        where TParentEntity : Entity
        where TChildEntity : ChildEntity
    {
        [Dependency]
        public IAlterRepository<TParentEntity> AlterRepository;

        [Dependency]
        public IEntityValidator<TParentEntity> EntityValidator;

        [Dependency]
        public IResourceValidator<TAlterResource, TParentEntity> ResourceValidator;

        [Dependency]
        public IParentManager<TParentEntity, TChildEntity> ParentManager;

        [Dependency]
        public IResourceMapper<TParentResource, TParentEntity> ResourceMapper;

        [Dependency]
        public IAlterResourceMapper<TAlterResource, TChildEntity> AlterResourceMapper;

        public virtual async Task<ActionResult<TParentResource>> Create(TAlterResource resource)
        {
            var validation = await EntityValidator.ValidateFound(resource.ParentId, nameof(resource.ParentId));
            if (validation != null)
                return GetValidationResult(validation);

            var parnet = await LookupRepository.Get(resource.ParentId);
            var validations = await ResourceValidator.ValidateReplace(resource, parnet);
            if (validations.Any())
                return GetValidationResult(validations);

            var child = await AlterResourceMapper.MapCreate(resource);
            ParentManager.Add(parnet, child);

            await AlterRepository.Replace(parnet);
            return await ResourceMapper.Map(parnet);
        }

        public virtual async Task<ActionResult<TParentResource>> Replace(string id, TAlterResource resource)
        {
            var validation = await EntityValidator.ValidateFound(resource.ParentId, nameof(resource.ParentId));
            if (validation != null)
                return GetValidationResult(validation);

            var parent = await LookupRepository.Get(resource.ParentId);
            var validations = await ResourceValidator.ValidateReplace(resource, parent);
            if (validations.Any())
                return GetValidationResult(validations);

            var child = await AlterResourceMapper.MapCreate(resource);
            ParentManager.Replace(parent, child, id);

            await AlterRepository.Replace(parent);
            return await ResourceMapper.Map(parent);
        }

        public virtual async Task<ActionResult<TParentResource>> Update(string id, TAlterResource resource)
        {
            var parent = await LookupRepository.Get(resource.ParentId);
            if (parent == null)
                return NotFound(resource.ParentId);
            if (!ParentManager.Exists(parent, id))
                return NotFound(id);

            var validations = await ResourceValidator.ValidateUpdate(resource, parent);
            if (validations.Any())
                return GetValidationResult(validations);

            var child = ParentManager.Get(parent, id);
            await AlterResourceMapper.MapUpdate(resource, child);
            ParentManager.Replace(parent, child, id);

            await AlterRepository.Update(parent);
            return await ResourceMapper.Map(parent);
        }

        public virtual async Task<ActionResult<TParentResource>> Delete(string parentId, string childId)
        {
            var parent = await LookupRepository.Get(parentId);
            if (parent == null)
                return NotFound(parentId);
            if (!ParentManager.Exists(parent, childId))
                return NotFound(childId);
            ParentManager.Remove(parent, childId);
            await AlterRepository.Update(parent);
            return await ResourceMapper.Map(parent);
        }

        public async virtual Task<ActionResult<TParentResource>> DeleteAll(string parentId)
        {
            var parent = await LookupRepository.Get(parentId);
            if (parent == null)
                return NotFound(parentId);
            ParentManager.RemoveAll(parent);
            await AlterRepository.Update(parent);
            return await ResourceMapper.Map(parent);
        }
    }
}