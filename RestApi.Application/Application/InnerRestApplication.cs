using RestApi.Common.Applications;
using RestApi.Common.Entities;
using RestApi.Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity;
using RestApi.Standard.Shared.Resources;
using RestApi.Standard.Shared.Resources.Users;
using System.Linq;
using RestApi.Standard.Shared.Errors;
using MongoDB.Bson;
using System;

namespace RestApi.Application.Application
{
    public abstract class InnerRestApplication<TCreateResource, TResource, TParentEntity, TEntity>
        : ApplicationBase, IInnerRestApplication<TCreateResource, TResource>
        where TCreateResource : CreateResource
        where TResource : Resource
        where TParentEntity : Entity
        where TEntity : Entity
    {
        [Dependency]
        public IRepository<TParentEntity> Repository { get; set; }

        public UserResource CurrentUser { get; set; }

        public virtual async Task<ActionResult<IEnumerable<TResource>>> Get(string parentId)
        {
            var parent = await Repository.Get(parentId);
            if (parent == null) return NotFound(parentId);
            return Ok(MapReturnValue(parent));
        }

        public virtual async Task<ActionResult<TResource>> Get(string parentId, string id)
        {
            var parent = await Repository.Get(parentId);
            if (parent == null) return NotFound(parentId);

            var entity = GetEntity(parent, id);
            if (entity == null)
                return NotFound(id);
            return Mapper.Map<TResource>(entity);
        }

        public virtual async Task<ActionResult<TResource>> Create(string parentId, TCreateResource createResource)
        {
            var parent = await Repository.Get(parentId);
            if (parent == null) return NotFound(parentId);

            var actionResult = await AddToParent(parent, createResource);
            if (actionResult.Result != null)
                return actionResult.Result;
            return await UpdateAndMap(parent, actionResult.Value);
        }

        public virtual async Task<ActionResult<TResource>> Update(string parentId, string id, TCreateResource resource)
        {
            var parent = await Repository.Get(parentId);
            if (parent == null) return NotFound(parentId);

            var entity = GetEntity(parent, id);
            if (entity == null)
                return NotFound(id);
            await UpdateItemOnParent(parent, entity, resource);
            return await UpdateAndMap(parent, entity);
        }

        public async virtual Task<ActionResult<TResource>> Delete(string parentId, string id)
        {
            var parent = await Repository.Get(parentId);
            if (parent == null) return NotFound(parentId);

            var actionResult = Delete(parent, id);
            if (actionResult.Result != null)
                return actionResult.Result;
            return await UpdateAndMap(parent, actionResult.Value);
        }

        protected TEntity GetEntity(TParentEntity parent, string id)
        {
            return GetEntities(parent).FirstOrDefault(c => c.Id == id);
        }

        protected abstract ICollection<TEntity> GetEntities(TParentEntity parent);

        protected virtual async Task<ActionResult<TEntity>> AddToParent(TParentEntity parent, TCreateResource createResource)
        {
            var entity = Mapper.Map<TEntity>(createResource);
            if (string.IsNullOrEmpty(entity.Id))
                entity.Id = ObjectId.GenerateNewId().ToString();
            AddToParent(parent, entity);
            return entity;
        }

        protected virtual void AddToParent(TParentEntity parent, TEntity entity)
        {
            GetEntities(parent).Add(entity);
        }

        protected virtual Task UpdateItemOnParent(TParentEntity parent, TEntity entity, TCreateResource resource)
        {
            Mapper.Map(resource, entity);
            return Task.CompletedTask;
        }

        protected virtual ActionResult<TEntity> Delete(TParentEntity parent, string id)
        {
            var entity = GetEntity(parent, id);
            if (entity == null)
                return NotFound(id);
            RemoveFromParent(parent, entity);
            return entity;
        }

        protected virtual void RemoveFromParent(TParentEntity parent, TEntity entity)
        {
            GetEntities(parent).Remove(entity);
        }

        protected virtual IEnumerable<TResource> MapReturnValue(TParentEntity parent)
        {
            return Mapper.Map<IEnumerable<TResource>>(GetEntities(parent));
        }

        protected async Task<ActionResult<TResource>> UpdateAndMap(TParentEntity parent, TEntity entity)
        {
            await Repository.Update(parent);
            return Mapper.Map<TResource>(entity);
        }
    }
}