using Core.Server.Common.Applications;
using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Mappers;
using Core.Server.Common.Repositories;
using Core.Server.Common.Validators;
using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application
{
    [Inject]
    public class ChildAlterApplication<TCreateResource, TUpdateResource, TParentResource, TParentEntity, TChildEntity>
        : BaseApplication<TParentEntity>,
          IChildAlterApplication<TCreateResource, TUpdateResource, TParentResource>
        where TCreateResource : ChildCreateResource
        where TUpdateResource : ChildUpdateResource
        where TParentResource : Resource
        where TParentEntity : Entity
        where TChildEntity : Entity
    {
        [Dependency]
        public IAlterRepository<TParentEntity> AlterRepository;

        [Dependency]
        public IResourceValidator<TCreateResource, TUpdateResource, TParentEntity> ResourceValidator;

        [Dependency]
        public IParentManager<TParentEntity, TChildEntity> ParentManager;

        [Dependency]
        public IResourceMapper<TParentResource, TParentEntity> ResourceMapper;

        [Dependency]
        public IAlterResourceMapper<TCreateResource, TUpdateResource, TChildEntity> AlterResourceMapper;

        public virtual async Task<ActionResult<TParentResource>> Create(TCreateResource resource)
        {
            var parnet = await LookupRepository.Get(resource.ParentId);
            if (parnet == null)
                return NotFound(resource.ParentId);

            var validation = await ResourceValidator.Validate(resource, parnet);
            if (!(validation is OkResult))
                return validation;

            var child = await AlterResourceMapper.Map(resource);
            child.Id = ObjectId.GenerateNewId().ToString();
            ParentManager.Add(parnet, child);

            await AlterRepository.Replace(parnet);
            return await ResourceMapper.Map(parnet);
        }

        public virtual async Task<ActionResult<TParentResource>> Replace(string id, TCreateResource resource)
        {
            var parent = await LookupRepository.Get(resource.ParentId);
            if (parent == null)
                return NotFound(resource.ParentId);

            var validation = await ResourceValidator.Validate(resource, parent);
            if (!(validation is OkResult))
                return validation;

            var child = await AlterResourceMapper.Map(resource);
            ParentManager.Replace(parent, child, id);

            await AlterRepository.Replace(parent);
            return await ResourceMapper.Map(parent);
        }

        public virtual async Task<ActionResult<TParentResource>> Update(string id, TUpdateResource resource)
        {
            var parent = await LookupRepository.Get(resource.ParentId);
            if (parent == null)
                return NotFound(resource.ParentId);
            if (!ParentManager.Exists(parent, id))
                return NotFound(id);

            var validation = await ResourceValidator.Validate(resource, parent);
            if (!(validation is OkResult))
                return validation;

            var child = ParentManager.Get(parent, id);
            await AlterResourceMapper.Map(resource, child);
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