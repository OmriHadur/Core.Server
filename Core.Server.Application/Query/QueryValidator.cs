using Core.Server.Shared.Errors;
using Core.Server.Shared.Resources;
using System.Linq;
using Core.Server.Injection.Attributes;
using System;
using System.Reflection;
using Core.Server.Common.Query;

namespace Core.Server.Application.Query
{
    [Inject]
    public class QueryValidator : QueringBase, IQueryValidator
    {
        public BadRequestReason? Validate<TResource>(QueryBase queryBase)
            where TResource : Resource
        {
            if (queryBase is QueryUnion)
            {
                var validations = (queryBase as QueryUnion)
                    .Queries.Select(qr => Validate<TResource>(qr));
                return validations.FirstOrDefault(v => v != null);
            }
            if (queryBase is QueryField)
            {
                var propertyInfo = GetPropertyNameInfo<TResource>(queryBase as QueryField);
                if (propertyInfo == null)
                    return BadRequestReason.PropertyNotFound;

                if (queryBase is QueryString && propertyInfo.PropertyType != typeof(string))
                    return BadRequestReason.PropertyNotCurectType;
                if (queryBase is QueryNumber && !IsNumeric(propertyInfo.PropertyType))
                    return BadRequestReason.PropertyNotCurectType;
            }

            var inRange = IsEnumInRange(queryBase);
            if (!inRange)
                return BadRequestReason.EnumNotInRange;

            return null;
        }

        private bool IsNumeric(Type type)
        {
            return type.IsPrimitive && type != typeof(char) && type != typeof(bool);
        }

        private PropertyInfo GetPropertyNameInfo<TResource>(QueryField queryField)
            where TResource : Resource
        {
            var propertyNameValue = queryField.Field;
            return GetPropertyInfo<TResource>(propertyNameValue);
        }

        private bool IsEnumInRange(QueryBase obj)
        {
            var propertyInfo = GetPropertyInfo("Operand", obj.GetType());
            if (propertyInfo == null) return true;
            var maxValue = Enum.GetValues(propertyInfo.PropertyType).Length;
            var value = (int)(propertyInfo.GetValue(obj));
            return 0 <= value && value < maxValue;
        }
    }
}
