using RestApi.Common;
using RestApi.Common.Query;
using RestApi.Shared.Errors;
using RestApi.Shared.Query;
using RestApi.Shared.Resources;
using System.Linq;
using System.Reflection;

namespace RestApi.Application.Helpers
{
    [Inject]
    public class QueryBuilder : IQueryBuilder
    {
        public BadRequestReason? Validate<TResource>(QueryResource queryResource)
            where TResource : Resource
        {
            if (queryResource is LogicQueryResource)
            {
                var validations = (queryResource as LogicQueryResource)
                    .QueryResources.Select(qr => Validate<TResource>(qr));
                return validations.FirstOrDefault(v => v != null);
            }
            else
            {
                var propertyName = (queryResource as PropertyQueryResource).PropertyName;
                var propertyInfo = GetPropertyInfo<TResource>(propertyName);
                if (propertyInfo == null)
                    return BadRequestReason.PropertyNotFound;
                if (queryResource is StringQueryResource)
                {
                    if (propertyInfo.PropertyType != typeof(string))
                        return BadRequestReason.PropertyNotCurectType;
                }
                else if (queryResource is NumberQueryResource)
                {
                    if (propertyInfo.PropertyType != typeof(int))
                        return BadRequestReason.PropertyNotCurectType;
                }
                return null;
            }
        }

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

        private static PropertyInfo GetPropertyInfo<TResource>(string propertyName) 
            where TResource : Resource
        {
            return typeof(TResource).GetProperties().FirstOrDefault(p => p.Name.ToLower() == propertyName);
        }

        private static string GetPropertyName<TResource>(string propertyName)
            where TResource : Resource
        {
            return GetPropertyInfo<TResource>(propertyName).Name;
        }

    }
}
