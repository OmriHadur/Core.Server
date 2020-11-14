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
    [Inject]
    public class ResourceMapper<TResource, TEntity>
        : IResourceMapper<TResource, TEntity>
        where TResource : Resource
        where TEntity : Entity
    {
        [Dependency]
        public IMapper Mapper { get; set; }

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
