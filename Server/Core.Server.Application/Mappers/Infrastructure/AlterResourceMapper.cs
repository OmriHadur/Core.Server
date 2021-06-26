using AutoMapper;
using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Mappers;
using Core.Server.Injection.Interfaces;
using Core.Server.Shared.Attributes;
using System.Linq;
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
        public IMapper Mapper;

        [Dependency]
        public IReflactionHelper ReflactionHelper;
        
        public virtual async Task<TEntity> MapCreate(TAlterResource resource)
        {
            return Mapper.Map<TEntity>(resource);
        }

        public virtual async Task MapReplace(TAlterResource resource, TEntity entity)
        {
            AssignImmutableValues(resource, entity);
            Mapper.Map(resource, entity);
        }

        public virtual async Task MapUpdate(TAlterResource resource, TEntity entity)
        {
            AssignImmutableValues(resource, entity);
            var updatedEntity = Mapper.Map<TEntity>(resource);
            UpdateEntity(entity, updatedEntity);
        }

        private static void UpdateEntity<T>(T entity, T updatedEntity)
        {
            foreach (var property in entity.GetType().GetProperties())
            {
                var value = property.GetValue(updatedEntity);
                if (value != null)
                {
                    var propertyType = property.PropertyType;
                    if (propertyType.IsPrimitive || propertyType.IsArray || propertyType == typeof(string))
                        property.SetValue(entity, value);
                    else
                        UpdateEntity(property.GetValue(entity), property.GetValue(updatedEntity));
                }
            }
        }

        protected void AssignImmutableValues(TAlterResource alterResource, TEntity entity)
        {
            var properties = ReflactionHelper.GetPropertiesWithAttribute<ImmutableAttribute>(alterResource);
            var entityProperties = entity.GetType().GetProperties();
            foreach (var property in properties)
            {
                var entityProperty = entityProperties.FirstOrDefault(p => p.Name == property.Name);
                if (entityProperty != null)
                {
                    var entityValue = entityProperty.GetValue(entity);
                    property.SetValue(alterResource, entityValue);
                }
            }
        }
    }
}
