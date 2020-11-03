using System.Collections.Generic;
using System.Linq;

namespace Core.Server.Common.Query
{
    public class QueryUnion : QueryBase
    {
        public QueryUnionOperands Operand { get; set; }
        public IEnumerable<QueryBase> Queries { get; set; }

        public override bool Equals(object obj)
        {
            var queryUnion = obj as QueryUnion;
            if (queryUnion == null) return false;
            return queryUnion.Operand == Operand
                && queryUnion.Queries.SequenceEqual(Queries);
        }

        public override int GetHashCode()
        {
            return Operand.GetHashCode() + Queries.Sum(q => q.GetHashCode());
        }
    }
}
