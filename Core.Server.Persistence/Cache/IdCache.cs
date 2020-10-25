using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Core.Server.Injection.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Persistence.Cache
{
    [Inject]
    public class IdCache<TEntity>
        : BaseCache
       , ILookupCachedRepository<TEntity>
        where TEntity : Entity
    {
        private readonly Dictionary<string, TEntity> idChahe;

        [Dependency]
        public ILookupRepository<TEntity> LookupRepository;

        public IdCache()
        {
            idChahe = new Dictionary<string, TEntity>();
        }

        public Task<bool> Any()
        {
            return LookupRepository.Any();
        }

        public async Task<bool> Exists(string id)
        {
            if (idChahe.ContainsKey(id))
                return true;
            return await LookupRepository.Exists(id);
        }

        public Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FindFirst(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> Get(string[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> Get()
        {
            throw new NotImplementedException();
        }

        public void Set(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Set(TEntity[] entities)
        {
            throw new NotImplementedException();
        }
    }
}
