using Core.Server.Common.Query;
using Core.Server.Shared.Query;
using Core.Server.Shared.Resources;
using System.Linq;
using Core.Server.Injection.Attributes;

namespace Core.Server.Application.Query
{
    [Inject]
    public class QueringBuilder
        : QueringBase
        , IQueringBuilder
    {
        public QueryBase Build<TResource>(QueryResource queryResource)
            where TResource : Resource
        {
            if (queryResource is StringQueryResource)
                return GetStringQuery<TResource>(queryResource as StringQueryResource);
            if (queryResource is NumberQueryResource)
                return GetNumberQuery<TResource>(queryResource as NumberQueryResource);
            else if (queryResource is LogicQueryResource)
                return GetLogicQuery<TResource>(queryResource as LogicQueryResource);
            return null;
        }

        private QueryBase GetNumberQuery<TResource>(NumberQueryResource numberQueryResource) where TResource : Resource
        {
            return new NumberQuery()
            {
                Field = GetPropertyName<TResource>(numberQueryResource.PropertyName),
                Value = numberQueryResource.Value,
                Operand = numberQueryResource.Operand
            };
        }

        private QueryBase GetLogicQuery<TResource>(LogicQueryResource logicQueryResource) where TResource : Resource
        {
            return new LogicQuery()
            {
                IsAnd = logicQueryResource.Operand == 0,
                Queries = logicQueryResource.QueryResources.Select(r => Build<TResource>(r))
            };
        }

        private QueryBase GetStringQuery<TResource>(StringQueryResource queryResource) where TResource : Resource
        {
            var stringQuery = new StringQuery()
            {
                Field = GetPropertyName<TResource>(queryResource.PropertyName),
                Regex = queryResource.Value
            };
            switch (queryResource.Operand)
            {
                case StringQueryOperands.StartsWith:
                    stringQuery.Regex = $"^{stringQuery.Regex}";
                    break;
                case StringQueryOperands.EndsWith:
                    stringQuery.Regex += "$";
                    break;
                case StringQueryOperands.Empty:
                    stringQuery.Regex = "^$";
                    break;
            }
            return stringQuery;
        }
    }
}
