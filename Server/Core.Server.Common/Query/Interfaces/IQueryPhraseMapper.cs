using Core.Server.Shared.Resources;

namespace Core.Server.Common.Query
{
    public interface IQueryPhraseMapper
    {
        QueryBase Map<TResource>(string queryPhrase)
            where TResource : Resource;
    }
}