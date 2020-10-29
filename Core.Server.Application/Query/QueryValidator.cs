using Core.Server.Shared.Errors;
using Core.Server.Shared.Query;
using Core.Server.Shared.Resources;
using System.Linq;
using Core.Server.Injection.Attributes;
using System;
using System.Reflection;

namespace Core.Server.Application.Query
{
    [Inject]
    public class QueringValidator
        : QueringBase
        , IQueringValidator
    {
        public BadRequestReason? Validate<TResource>(QueryResource queryResource)
            where TResource : Resource
        {
            if (queryResource is LogicQueryResource)
            {
                var validations = (queryResource as LogicQueryResource)
                    .QueryResources.Select(qr => Validate<TResource>(qr));
                return validations.FirstOrDefault(v => v != null);
            }
            else
            {
                var propertyInfo = GetPropertyNameInfo<TResource>(queryResource);
                if (propertyInfo == null)
                    return BadRequestReason.PropertyNotFound;

                var inRange = IsEnumInRange(queryResource);
                if (!inRange)
                    return BadRequestReason.EnumNotInRange;

                if (queryResource is StringQueryResource)
                {
                    if (propertyInfo.PropertyType != typeof(string))
                        return BadRequestReason.PropertyNotCurectType;
                }
                else if (queryResource is NumberQueryResource)
                {
                    if (propertyInfo.PropertyType != typeof(int))
                        return BadRequestReason.PropertyNotCurectType;
                }
                return null;
            }
        }

        private PropertyInfo GetPropertyNameInfo<TResource>(QueryResource queryResource) where TResource : Resource
        {
            var propertyNameValue = (queryResource as PropertyQueryResource).PropertyName;
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
