using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Server.Common.Query.Infrastructure
{
    public class QueryRequest
    {
        public QueryBase Query { get; set; }
        public string OrderBy { get; set; }
        public bool IsDecending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public bool HasOrdering => !string.IsNullOrEmpty(OrderBy);
        public bool HasPaging => Page != 0;
    }
}
