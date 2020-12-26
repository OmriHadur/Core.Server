using MongoDB.Driver;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Unity;
using Core.Server.Common.Attributes;

namespace Core.Server.Persistence.Repositories
{
    [Inject]
    public class LookupRepository<TEntity>
        : BaseRepository<TEntity>,
          ILookupRepository<TEntity>
        where TEntity : Entity
    {
        public async Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return (await Collection.FindAsync(predicate)).ToList();
        }

        public async Task<TEntity> FindFirst(Expression<Func<TEntity, bool>> predicate)
        {
            return (await Collection.FindAsync(predicate)).FirstOrDefault();
        }

        public async Task<TEntity> Get(string id)
        {
            return (await Collection.FindAsync(e => e.Id == id)).FirstOrDefault();
        }

        public async Task<IEnumerable<TEntity>> Get(string[] ids)
        {
            return (await Collection.FindAsync(e => ids.Contains(e.Id))).ToList();
        }

        public async Task<IEnumerable<TEntity>> FindAll(Func<TEntity, bool> findFunc)
        {
            return (await Collection.FindAsync(e => findFunc(e))).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> Get()
        {
            return (await Collection.FindAsync(e => true)).ToList();
        }

        public async Task<bool> Exists(string id)
        {
            var answer = await Collection.FindAsync(e => e.Id == id);
            return answer.FirstOrDefault() != null;
        }

        public async Task<string> GetNotFoundId(string[] ids)
        {
            var entities = (await Collection.FindAsync(e => ids.Contains(e.Id))).ToList();
            return ids.FirstOrDefault(id => !entities.Any(e => e.Id == id));
        }

        public async Task<bool> Any()
        {
            var answer = await Collection.FindAsync(e => true);
            return answer.FirstOrDefault() != null;
        }

        public async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
        {
            var answer = await Collection.FindAsync(predicate);
            return answer.FirstOrDefault() != null;
        }
    }
}
