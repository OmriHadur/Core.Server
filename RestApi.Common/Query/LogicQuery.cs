using System.Collections.Generic;

namespace RestApi.Common.Query
{
    public class LogicQuery : QueryBase
    {
        public bool IsAnd{ get; set; }
        public IEnumerable<QueryBase> Queries{ get; set; }
    }
}
