using Core.Server.Common.Query.Infrastructure;
using Core.Server.Shared.Query;
using Core.Server.Shared.Resources;

namespace Core.Server.Common.Query
{
    public interface IQueryResourceMapper
    {
        QueryRequest Map<TResource>(QueryResource queryResource) where TResource : Resource;
    }
}