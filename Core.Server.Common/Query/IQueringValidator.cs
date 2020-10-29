using Core.Server.Shared.Errors;
using Core.Server.Shared.Query;
using Core.Server.Shared.Resources;

namespace Core.Server.Application.Query
{
    public interface IQueringValidator
    {
        BadRequestReason? Validate<TResource>(QueryResource queryResource) 
            where TResource : Resource;
    }
}