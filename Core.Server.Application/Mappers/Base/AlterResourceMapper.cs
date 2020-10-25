using AutoMapper;
using Core.Server.Injection.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Mappers;
using Core.Server.Shared.Resources;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application.Mappers.Base
{
    [Inject]
    public class AlterResourceMapper<TCreateResource, TUpdateResource, TEntity>
        : IAlterResourceMapper<TCreateResource, TUpdateResource, TEntity>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TEntity : Entity
    {
        [Dependency]
        public IMapper Mapper { get; set; }

        public virtual async Task<TEntity> Map(TCreateResource resource)
        {
            return Mapper.Map<TEntity>(resource);
        }

        public virtual async Task Map(TCreateResource resource, TEntity entity)
        {
            Mapper.Map(resource, entity);
        }

        public virtual async Task Map(TUpdateResource resource, TEntity entity)
        {
            var updatedEntity = Mapper.Map<TEntity>(resource);
            foreach (var property in typeof(TEntity).GetProperties())
            {
                var value = property.GetValue(updatedEntity);
                if (value != null)
                    property.SetValue(entity, value);
            }
        }
    }
}
