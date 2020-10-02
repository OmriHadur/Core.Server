using Core.Server.Common.Entities;
using Core.Server.Common.Query;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Server.Common.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : Entity
    {
        Task Add(TEntity entity);

        Task AddRange(IEnumerable<TEntity> entities);

        Task<TEntity> Get(string id);

        Task<IEnumerable<TEntity>> GetAll(IEnumerable<string> ids);

        Task<List<TEntity>> Get();

        Task<List<TEntity>> Query(QueryBase query);

        Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FindFirst(Expression<Func<TEntity, bool>> predicate);

        Task DeleteMany(Expression<Func<TEntity, bool>> predicate);
        Task DeleteOne(Expression<Func<TEntity, bool>> predicate);

        Task<bool> Exists(Expression<Func<TEntity, bool>> predicate);

        Task RemoveRange(IEnumerable<TEntity> entities);

        Task Update(TEntity item);

        Task<bool> IsExists(string id);
        Task<bool> IsExists(Expression<Func<TEntity, bool>> predicate);
        Task Delete(TEntity entity);
    }
}
