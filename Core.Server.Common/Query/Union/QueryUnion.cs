using System.Collections.Generic;

namespace Core.Server.Common.Query
{
    public class QueryUnion : QueryBase
    {
        public QueryUnionOperands Operand { get; set; }
        public IEnumerable<QueryBase> Queries { get; set; }
    }
}
