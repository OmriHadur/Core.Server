using System.Collections.Generic;

namespace Core.Server.Common.Query
{
    public interface IQueryPhrasePipelineMapper
    {
        QueryBase Map(string queryPhrase, IEnumerable<IQueryPhrasePipelineMapper> mappers);

        int Priory { get; }
    }
}