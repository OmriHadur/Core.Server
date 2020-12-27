using Core.Server.Application;
using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Common.Validators
{
    [Inject]
    public class BatchResourceValidator<TAlterResource, TEntity>
        : BaseApplication<TEntity>,
          IBatchResourceValidator<TAlterResource, TEntity>
        where TEntity : Entity
    {
        [Dependency]
        public IResourceValidator<TAlterResource, TEntity> ResourceValidator;

        [Dependency]
        public IEntityValidator<TEntity> EntityValidator;

        public async Task<IEnumerable<StringKeyValuePair>> ValidateCreate(TAlterResource[] resources)
        {
            var validation = new List<StringKeyValuePair>();
            for (int i = 0; i < resources.Length; i++)
            {
                var resourceValidations = await ResourceValidator.ValidateCreate(resources[i]);
                resourceValidations = resourceValidations.Select(r =>
                {
                    r.Key = $"[{i}]." + r.Key;
                    return r;
                });
                validation.AddRange(resourceValidations);
            }
            return validation;
        }

        public Task<IEnumerable<StringKeyValuePair>> ValidateReplace(TAlterResource[] createResource, TEntity[] entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<StringKeyValuePair>> ValidateUpdate(TAlterResource[] updateResource, TEntity[] entity)
        {
            throw new System.NotImplementedException();
        }
    }
}