using Core.Server.Application;
using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Injection.Interfaces;
using Core.Server.Shared.Attributes;
using System;
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
            var validationImmutableAttribute = GetNullProperties<ImmutableAttribute>(alterResource);
            var validationAlterAttribute = GetNullProperties<RequiredOnAlterAttribute>(alterResource);
            return validationImmutableAttribute.Union(validationAlterAttribute);
        }

        public virtual async Task<IEnumerable<StringKeyValuePair>> ValidateReplace(TAlterResource alterResource, TEntity entity)
        {
            var validationAlterAttribute = GetNullProperties<RequiredOnAlterAttribute>(alterResource);
            var validationAlter = await ValidateAlter(alterResource, entity);
            return validationAlterAttribute.Union(validationAlter);
        }

        public virtual Task<IEnumerable<StringKeyValuePair>> ValidateUpdate(TAlterResource alterResource, TEntity entity)
        {
            return ValidateAlter(alterResource, entity);
        }

        protected virtual async Task<IEnumerable<StringKeyValuePair>> ValidateAlter(TAlterResource alterResource, TEntity entity)
        {
            return GetNonNullImmutableProperties(alterResource);
        }

        protected IEnumerable<StringKeyValuePair> GetNullProperties<TAttribute>(TAlterResource createResource)
            where TAttribute : Attribute
        {
            var properties = ReflactionHelper.GetPropertiesWithAttribute<TAttribute>(createResource);
            foreach (var property in properties)
                if (property.GetValue(createResource) == null)
                    yield return new StringKeyValuePair(property.Name, $"The {property.Name} field is required.");
        }

        protected IEnumerable<StringKeyValuePair> GetNonNullImmutableProperties(TAlterResource createResource)
        {
            var properties = ReflactionHelper.GetPropertiesWithAttribute<ImmutableAttribute>(createResource);
            foreach (var property in properties)
                if (property.GetValue(createResource) != null)
                    yield return new StringKeyValuePair(property.Name, $"The {property.Name} field is immutable and cannot be assign.");
        }

        protected void AddValidation(IList<StringKeyValuePair> validation, string key, string value)
        {
            validation.Add(new StringKeyValuePair(key, value));
        }

        protected void AddValidationAlreadyExists(IList<StringKeyValuePair> validation, string key)
        {
            validation.Add(new StringKeyValuePair(key, key + " already exists"));
        }

        protected void AddValidationUnauthorized(IList<StringKeyValuePair> validation, string key)
        {
            validation.Add(new StringKeyValuePair(key, "Unauthorized to change values of " + key));
        }

        protected void AddValidationInvalid(IList<StringKeyValuePair> validation, string key)
        {
            validation.Add(new StringKeyValuePair(key, "Invalid value of " + key));
        }
    }
}