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
    public class QueringValidator
        : QueringBase
        , IQueryBaseValidator
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
            else
            {
                var propertyInfo = GetPropertyNameInfo<TResource>(queryBase);
                if (propertyInfo == null)
                    return BadRequestReason.PropertyNotFound;

                var inRange = IsEnumInRange(queryBase);
                if (!inRange)
                    return BadRequestReason.EnumNotInRange;

                if (queryBase is QueryString)
                {
                    if (propertyInfo.PropertyType != typeof(string))
                        return BadRequestReason.PropertyNotCurectType;
                }
                else if (queryBase is QueryNumber)
                {
                    if (propertyInfo.PropertyType != typeof(int))
                        return BadRequestReason.PropertyNotCurectType;
                }
                return null;
            }
        }

        private PropertyInfo GetPropertyNameInfo<TResource>(QueryBase queryResource)
            where TResource : Resource
        {
            var propertyNameValue = (queryResource as QueryField).Field;
            return GetPropertyInfo<TResource>(propertyNameValue);
        }

        private bool IsEnumInRange(object obj)
        {
            var propertyInfo = GetPropertyInfo("operand", obj.GetType());
            if (propertyInfo == null) return true;
            var maxValue = Enum.GetValues(propertyInfo.PropertyType).Length;
            var value = (int)(propertyInfo.GetValue(obj));
            return 0 < value && value < maxValue;
        }
    }
}
