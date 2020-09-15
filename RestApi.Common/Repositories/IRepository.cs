using RestApi.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RestApi.Common.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : Entity
    {
        Task Add(TEntity entity);

        Task AddRange(IEnumerable<TEntity> entities);

        Task<TEntity> Get(string id);

        Task<IEnumerable<TEntity>> GetAll(IEnumerable<string> ids);

        Task<List<TEntity>> GetAll();

        Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FindFirst(Expression<Func<TEntity, bool>> predicate);
        Task<bool> Exists(Expression<Func<TEntity, bool>> predicate);

        Task RemoveRange(IEnumerable<TEntity> entities);

        Task Update(TEntity item);

        Task<bool> Exists(string id);
        Task Delete(TEntity entity);
    }
}
