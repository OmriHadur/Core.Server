using Core.Server.Common.Query;
using Core.Server.Shared.Errors;
using Core.Server.Shared.Query;
using Core.Server.Shared.Resources;

namespace Core.Server.Application.Helpers
{
    public interface IQueryBuilder
    {
        QueryBase Build<TResource>(QueryResource queryResource)
            where TResource : Resource;
        BadRequestReason? Validate<TResource>(QueryResource queryResource) 
            where TResource : Resource;
    }
}