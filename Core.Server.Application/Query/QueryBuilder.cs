using Core.Server.Common.Query;
using Core.Server.Shared.Resources;
using Core.Server.Injection.Attributes;

namespace Core.Server.Application.Query
{
    [Inject]
    public class QueringMapper : QueringBase, IQueryMapper
    {
        public QueryBase Map<TResource>(string query)
            where TResource : Resource
        {
            return new QueryNumber() { Field = "Value", Value = 2, Operand = QueryNumberOperands.GreaterThen };
        }
    }
}
