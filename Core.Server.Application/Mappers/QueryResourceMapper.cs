using Core.Server.Common.Query;
using Core.Server.Common.Query.Infrastructure;
using Core.Server.Injection.Attributes;
using Core.Server.Shared.Query;
using Core.Server.Shared.Resources;
using Unity;

namespace Core.Server.Application.Mappers
{
    [Inject]
    public class QueryResourceMapper : IQueryResourceMapper
    {
        [Dependency]
        public IQueryPhraseMapper QueryPhraseMapper;

        public QueryRequest Map<TResource>(QueryResource queryResource)
            where TResource : Resource
        {
            var queryBase = QueryPhraseMapper.Map<TResource>(queryResource.QueryPhrase);

            return new QueryRequest()
            {
                Query = queryBase,
                OrderBy = GetOrderBy(queryResource),
                IsDecending = string.IsNullOrEmpty(queryResource.OrderBy),
                Page = queryResource.Page,
                PageSize = queryResource.PageSize
            };
        }

        private string GetOrderBy(QueryResource queryResource)
        {
            var orderByPropery = string.IsNullOrEmpty(queryResource.OrderBy) ? queryResource.OrderByDecending : queryResource.OrderBy;
            return ToStartUpper(orderByPropery);
        }

        private string ToStartUpper(string str)
        {
            return str[0].ToString().ToUpper() + str[1..];
        }
    }
}
