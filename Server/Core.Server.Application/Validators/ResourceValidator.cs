using Core.Server.Application;
using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Injection.Interfaces;
using Core.Server.Shared.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Common.Validators
{
    [Inject]
    public class ResourceValidator<TAlterResource, TEntity>
        : BaseApplication<TEntity>,
          IResourceValidator<TAlterResource, TEntity>
        where TEntity : Entity
    {
        [Dependency]
        public IReflactionHelper ReflactionHelper;

        public virtual async Task<IEnumerable<StringKeyValuePair>> ValidateCreate(TAlterResource alterResource)
        {
            return GetValidateCreate(alterResource);
        }

        public virtual Task<IEnumerable<StringKeyValuePair>> ValidateReplace(TAlterResource alterResource, TEntity entity)
        {
            return ValidateAlter(alterResource, entity);
        }

        public virtual Task<IEnumerable<StringKeyValuePair>> ValidateUpdate(TAlterResource alterResource, TEntity entity)
        {
            return ValidateAlter(alterResource, entity);
        }

        protected virtual async Task<IEnumerable<StringKeyValuePair>> ValidateAlter(TAlterResource alterResource, TEntity entity)
        {
            return new StringKeyValuePair[0];
        }

        protected IEnumerable<StringKeyValuePair> GetValidateCreate(TAlterResource createResource)
        {
            var properties = ReflactionHelper.GetPropertiesWithAttribute<ImmutableAttribute>(createResource);
            if (properties.Any())
                foreach (var property in properties)
                    if (property.GetValue(createResource) == null)
                        yield return new StringKeyValuePair(property.Name, $"The {property.Name} field is required.");
        }

        protected void AddValidation(IList<StringKeyValuePair> validation, string key, string value)
        {
            validation.Add(new StringKeyValuePair(key, value));
        }
    }
}