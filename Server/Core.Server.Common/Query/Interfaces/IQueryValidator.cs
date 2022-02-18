using Core.Server.Shared.Errors;
using Core.Server.Shared.Resources;

namespace Core.Server.Common.Query
{
    public interface IQueryValidator
    {
        BadRequestReason? Validate<TResource>(QueryBase queryResource)
            where TResource : Resource;
    }
}