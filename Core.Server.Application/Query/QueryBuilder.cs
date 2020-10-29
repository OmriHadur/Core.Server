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
        , IQueryResourceToEntityMapper
    {
        public QueryEntityBase Map<TResource>(QueryPropertyResource queryResource)
            where TResource : Resource
        {
            if (queryResource is StringQueryResource)
                return GetStringQuery<TResource>(queryResource as StringQueryResource);
            if (queryResource is NumberPropertyQueryResource)
                return GetNumberQuery<TResource>(queryResource as NumberPropertyQueryResource);
            else if (queryResource is LogicQueryResource)
                return GetLogicQuery<TResource>(queryResource as LogicQueryResource);
            return null;
        }

        private QueryEntityBase GetNumberQuery<TResource>(NumberPropertyQueryResource numberQueryResource) where TResource : Resource
        {
            return new NumberEntityQuery()
            {
                Field = GetPropertyName<TResource>(numberQueryResource.PropertyName),
                Value = numberQueryResource.Value,
                Operand = numberQueryResource.Operand
            };
        }

        private QueryEntityBase GetLogicQuery<TResource>(LogicQueryResource logicQueryResource) where TResource : Resource
        {
            return new LogicEntityQuery()
            {
                IsAnd = logicQueryResource.Operand == 0,
                Queries = logicQueryResource.QueryResources.Select(r => Map<TResource>(r))
            };
        }

        private QueryEntityBase GetStringQuery<TResource>(StringQueryResource queryResource) where TResource : Resource
        {
            var stringQuery = new StringEntityQuery()
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
