using System.Collections.Generic;

namespace Core.Server.Common.Query
{
    public class LogicQuery : QueryBase
    {
        public bool IsAnd{ get; set; }
        public IEnumerable<QueryBase> Queries{ get; set; }
    }
}
