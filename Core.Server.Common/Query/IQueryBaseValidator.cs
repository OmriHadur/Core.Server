using Core.Server.Shared.Errors;
using Core.Server.Shared.Query;
using Core.Server.Shared.Resources;

namespace Core.Server.Application.Query
{
    public interface IQueryBaseValidator
    {
        BadRequestReason? Validate<TResource>(QueryPropertyResource queryResource) 
            where TResource : Resource;
    }
}