using Core.Server.Common.Query;
using Core.Server.Shared.Errors;
using Core.Server.Shared.Resources;

namespace Core.Server.Application.Query
{
    public interface IQueryBaseValidator
    {
        BadRequestReason? Validate<TResource>(QueryBase queryResource) 
            where TResource : Resource;
    }
}