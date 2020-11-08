using Core.Server.Shared.Errors;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using Core.Server.Common.Query.Infrastructure;
using Core.Server.Common.Query;

namespace Core.Server.Application.Query
{
    [Inject]
    public class QueryResourceValidator : QueringBase, IQueryResourceValidator
    {
        public BadRequestReason? Validate<TResource>(QueryRequest queryRequest)
            where TResource: Resource
        {
            if (queryRequest.Page != 0 && queryRequest.PageSize == 0)
                return BadRequestReason.EnumNotInRange;

            return ValidateProperty<TResource>(queryRequest.OrderBy);
        }

        private BadRequestReason? ValidateProperty<TResource>(string propertyName)
            where TResource : Resource
        {
            if (!string.IsNullOrEmpty(propertyName)){
                var propertyInfo = GetPropertyInfo<TResource>(propertyName);
                if (propertyInfo == null)
                    return BadRequestReason.PropertyNotFound;
            }
            return null;
        }
    }
}
