using Core.Server.Common;
using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Core.Server.Common.Validators;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application.Validators
{
    [Inject]
    public class EntityValidator<TEntity> : IEntityValidator<TEntity>
        where TEntity : Entity
    {
        [Dependency]
        public ILookupRepository<TEntity> LookupRepository;

        public async Task<IEnumerable<StringKeyValuePair>> ValidateFound(string[] resources, string propertyName)
        {
            if (resources.Length != 0)
            {
                var notFoundIds = await LookupRepository.GetNotFoundIds(resources);
                if (notFoundIds != null && notFoundIds.Any())
                    return GetNotFound(propertyName, notFoundIds);
            }
            return new StringKeyValuePair[0];
        }

        public async Task<StringKeyValuePair> ValidateFound(string resourceId, string propertyName)
        {
            var notFoundId = await LookupRepository.Exists(resourceId);
            return !notFoundId ?
                GetNotFound(propertyName, resourceId) : 
                null;
        }

        private IEnumerable<StringKeyValuePair> GetNotFound(string propertyName, IEnumerable<string> notFoundIds)
        {
            foreach (var notFoundId in notFoundIds)
                yield return GetNotFound(propertyName, notFoundId);
        }

        private StringKeyValuePair GetNotFound(string propertyName, string id)
        {
            return new StringKeyValuePair(propertyName, $"Id {id} was not found");
        }
    }
}
