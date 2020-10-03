using AutoMapper;
using Core.Server.Common;
using Core.Server.Common.Entities;
using Core.Server.Common.Mappers;
using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core.Server.Application.Mappers.Base
{
    [InjectMany]
    public class AlterResourceMapper<TCreateResource, TUpdateResource, TResource, TEntity>
        : ResourceMapper<TResource, TEntity>,
         IAlterResourceMapper<TCreateResource, TUpdateResource, TResource, TEntity>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
        where TEntity : Entity, new()
    {
        public virtual void AddAutoMapping(Profile profile)
        {
            profile.CreateMap<TCreateResource, TEntity>();
            profile.CreateMap<TEntity, TResource>();
        }

        public virtual async Task<ActionResult<TEntity>> Map(TCreateResource resource)
        {
            return Mapper.Map<TEntity>(resource);
        }

        public virtual async Task<ActionResult<TEntity>> Map(TUpdateResource resource, TEntity entity)
        {
            return Mapper.Map<TEntity>(resource);
        }
    }
}
