using Core.Server.Common.Query;
using Core.Server.Shared.Resources;

namespace Core.Server.Application.Query
{
    public interface IQueryMapper
    {
        QueryBase Map<TResource>(string query)
            where TResource : Resource;
    }
}