using Core.Server.Common.Query;
using Core.Server.Shared.Resources;

namespace Core.Server.Application.Query
{
    public interface IQueryPhraseMapper
    {
        QueryBase Map<TResource>(string queryPhrase)
            where TResource : Resource;
    }
}