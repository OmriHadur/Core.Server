using System.Collections.Generic;

namespace Core.Server.Common.Query
{
    public class LogicEntityQuery : QueryEntityBase
    {
        public bool IsAnd{ get; set; }
        public IEnumerable<QueryEntityBase> Queries{ get; set; }
    }
}
