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

        public override bool Equals(object obj)
        {
            var queryRequest = obj as QueryRequest;
            if (queryRequest == null) return false;

            return Query.Equals(queryRequest.Query)
                && IsPageEquls(queryRequest)
                && IsOrderingEquls(queryRequest);
        }

        private bool IsOrderingEquls(QueryRequest queryRequest)
        {
            return (!HasOrdering && !queryRequest.HasOrdering) || (OrderBy == queryRequest.OrderBy && IsDecending == queryRequest.IsDecending);
        }

        private bool IsPageEquls(QueryRequest queryRequest)
        {
            return (!HasPaging && !queryRequest.HasPaging) || (Page == queryRequest.Page && PageSize == queryRequest.PageSize);
        }

        public override int GetHashCode()
        {
            return Query.GetHashCode()
                + (HasOrdering ? OrderBy.GetHashCode() : 0)
                + (IsDecending ? 1 : 2)
                + Page
                + PageSize;
        }
    }
}