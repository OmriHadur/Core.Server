using Core.Server.Common.Applications;
using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Mappers;
using Core.Server.Common.Repositories;
using Core.Server.Common.Validators;
using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application
{
    [Inject]
    public class ChildAlterApplication<TCreateResource, TUpdateResource, TResource, TEntity>
        : BaseApplication<TEntity>,
          IChildAlterApplication<TCreateResource, TUpdateResource, TResource>
        where TCreateResource : ChildCreateResource
        where TUpdateResource : ChildUpdateResource
        where TResource : Resource
        where TEntity : Entity
    {
        [Dependency]
        public IAlterRepository<TEntity> AlterRepository;

        [Dependency]
        public IResourceValidator<TCreateResource, TUpdateResource, TEntity> ResourceValidator;

        [Dependency]
        public IAlterChildResourceMapper<TCreateResource, TUpdateResource, TEntity> AlterResourceMapper;

        [Dependency]
        public IResourceMapper<TResource, TEntity> ResourceMapper;

        public virtual async Task<ActionResult<TResource>> Create(TCreateResource resource)
        {
            var parnet = await LookupRepository.Get(resource.ParentId);
            if (parnet == null)
                return NotFound(resource.ParentId);

            var validation = await ResourceValidator.Validate(resource, parnet);
            if (!(validation is OkResult))
                return validation;

            AlterResourceMapper.Add(resource, parnet);

            await AlterRepository.Add(parnet);
            return await ResourceMapper.Map(parnet);
        }

        public virtual async Task<ActionResult<TResource>> Replace(string id, TCreateResource resource)
        {
            var parent = await LookupRepository.Get(resource.ParentId);
            if (parent == null)
                return NotFound(resource.ParentId);

            var validation = await ResourceValidator.Validate(resource, parent);
            if (!(validation is OkResult))
                return validation;

            AlterResourceMapper.Map(resource, id, parent);
            await AlterRepository.Replace(parent);
            return await ResourceMapper.Map(parent);
        }

        public virtual async Task<ActionResult<TResource>> Update(string id, TUpdateResource resource)
        {
            var parent = await LookupRepository.Get(resource.ParentId);
            if (parent == null)
                return NotFound(resource.ParentId);
            if (!AlterResourceMapper.Exists(parent, id))
                return NotFound(id);

            var validation = await ResourceValidator.Validate(resource, parent);
            if (!(validation is OkResult))
                return validation;

            await AlterRepository.Update(parent);
            return await ResourceMapper.Map(parent);
        }

        public virtual async Task<ActionResult<TResource>> Delete(string parentId, string childId)
        {
            var parent = await LookupRepository.Get(parentId);
            if (parent == null)
                return NotFound(parentId);
            if (!AlterResourceMapper.Exists(parent, childId))
                return NotFound(childId);
            AlterResourceMapper.Remove(parent,childId);
            await AlterRepository.Update(parent);
            return await ResourceMapper.Map(parent);
        }

        public async virtual Task<ActionResult<TResource>> DeleteAll(string parentId)
        {
            var parent = await LookupRepository.Get(parentId);
            if (parent == null)
                return NotFound(parentId);
            AlterResourceMapper.RemoveAll(parent);
            await AlterRepository.Update(parent);
            return await ResourceMapper.Map(parent);
        }
    }
}