using AutoMapper;
using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Mappers;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application.Mappers.Base
{
    [Inject]
    public class AlterResourceMapper<TAlterResource, TEntity>
        : IAlterResourceMapper<TAlterResource, TEntity>
        where TEntity : Entity
    {
        [Dependency]
        public IMapper Mapper { get; set; }

        public virtual async Task<TEntity> MapCreate(TAlterResource resource)
        {
            return Mapper.Map<TEntity>(resource);
        }

        public virtual async Task MapCreate(TAlterResource resource, TEntity entity)
        {
            Mapper.Map(resource, entity);
        }

        public virtual async Task MapUpdate(TAlterResource resource, TEntity entity)
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
