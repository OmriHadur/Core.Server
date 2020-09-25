using RestApi.Common.Query;
using RestApi.Shared.Errors;
using RestApi.Shared.Query;
using RestApi.Shared.Resources;

namespace RestApi.Application.Helpers
{
    public interface IQueryBuilder
    {
        QueryBase Build<TResource>(QueryResource queryResource)
            where TResource : Resource;
        BadRequestReason? Validate<TResource>(QueryResource queryResource) 
            where TResource : Resource;
    }
}