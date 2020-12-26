using Core.Server.Common.Query;
using Core.Server.Common.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace Core.Server.Application.Query.PhraseBuilders
{
    [InjectName]
    public class UnionQueryPhrasePipelineMapper : QueryPhrasePipelineMapper
    {
        public override int Priory => 1;

        protected override string MappingRegex => "( \\| | & )";

        protected override QueryBase InnerMap(string queryPhrase, IEnumerable<IQueryPhrasePipelineMapper> mappers)
        {
            var splited = queryPhrase.Split('|', '&');
            var queries = splited.Select(s =>
            {
                return CallNext(s.Trim(), mappers);
            }).ToList();
            var operand = queryPhrase.Contains('|') ? QueryUnionOperands.Or : QueryUnionOperands.And;
            return new QueryUnion() { Queries = queries, Operand = operand };
        }
    }
}
