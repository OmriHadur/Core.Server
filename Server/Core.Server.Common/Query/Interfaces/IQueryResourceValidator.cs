using Core.Server.Common.Query.Infrastructure;
using Core.Server.Shared.Errors;
using Core.Server.Shared.Resources;

namespace Core.Server.Common.Query
{
    public interface IQueryResourceValidator
    {
        BadRequestReason? Validate<TResource>(QueryRequest queryRequest)
            where TResource : Resource;
    }
}