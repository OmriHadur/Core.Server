using Core.Server.Common.Query;
using Core.Server.Shared.Query;
using Core.Server.Shared.Resources;

namespace Core.Server.Application.Query
{
    public interface IQueryResourceToEntityMapper
    {
        QueryEntityBase Map<TResource>(QueryPropertyResource queryResource)
            where TResource : Resource;
    }
}