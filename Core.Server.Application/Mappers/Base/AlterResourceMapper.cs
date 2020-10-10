using AutoMapper;
using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Mappers;
using Core.Server.Shared.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application.Mappers.Base
{
    [InjectBoundle]
    public class AlterResourceMapper<TCreateResource, TUpdateResource, TResource, TEntity>
        : IAlterResourceMapper<TCreateResource, TUpdateResource, TResource, TEntity>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
        where TEntity : Entity, new()
    {
        [Dependency]
        public IMapper Mapper { get; set; }

        public virtual async Task<TEntity> Map(TCreateResource resource)
        {
            return Mapper.Map<TCreateResource, TEntity>(resource);
        }

        public virtual async Task<TEntity> Map(TUpdateResource resource, TEntity entity)
        {
            return Mapper.Map<TUpdateResource, TEntity>(resource);
        }

        public virtual async Task<TResource> Map(TEntity entity)
        {
            return Mapper.Map<TResource>(entity);
        }

        public virtual async Task<IEnumerable<TResource>> Map(IEnumerable<TEntity> entities)
        {
            return Mapper.Map<IEnumerable<TResource>>(entities);
        }
    }
}
