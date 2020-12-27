using Core.Server.Common.Applications;
using Core.Server.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Server.Common.Validators
{
    public interface IBatchResourceValidator<TAlterResource, TEntity>
       : IBaseApplication
        where TEntity : Entity
    {
        Task<IEnumerable<StringKeyValuePair>> ValidateCreate(TAlterResource[] createResource);

        Task<IEnumerable<StringKeyValuePair>> ValidateReplace(TAlterResource[] createResource, TEntity[] entity);

        Task<IEnumerable<StringKeyValuePair>> ValidateUpdate(TAlterResource[] updateResource, TEntity[] entity);
    }
}
