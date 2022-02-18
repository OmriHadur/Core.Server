using Core.Server.Common.Attributes;
using Core.Server.Common.Query;
using Core.Server.Shared.Resources;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace Core.Server.Application.Query
{
    [Inject]
    public class QueryPhraseMapper : QueringBase, IQueryPhraseMapper
    {
        [Dependency]
        public IEnumerable<IQueryPhrasePipelineMapper> PipelineMappers;

        public QueryBase Map<TResource>(string queryPhrase)
            where TResource : Resource
        {
            var mappersByPriory = PipelineMappers.Distinct().OrderBy(p => p.Priory).ToList();
            return mappersByPriory.First().Map(queryPhrase, mappersByPriory.Skip(1));
        }
    }
}
