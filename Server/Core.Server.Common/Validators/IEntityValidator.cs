using Core.Server.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Server.Common.Validators
{
    public interface IEntityValidator<TEntity>
        where TEntity : Entity
    {
        Task<IEnumerable<StringKeyValuePair>> ValidateFound(string[] resources, string propertyName);
        Task<StringKeyValuePair> ValidateFound(string resource, string propertyName);
    }
}
