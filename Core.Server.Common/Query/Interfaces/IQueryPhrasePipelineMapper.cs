using Core.Server.Common.Query;
using Core.Server.Shared.Resources;
using System.Collections.Generic;

namespace Core.Server.Application.Query
{
    public interface IQueryPhrasePipelineMapper
    {
        QueryBase Map(string queryPhrase, IEnumerable<IQueryPhrasePipelineMapper> mappers);

        int Priory { get; }
    }
}