using Core.Server.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Server.Common.Repositories
{
    public interface IRestRepository<TEntity>
        : IQueryRepository<TEntity>
        where TEntity : Entity
    {
        Task Add(TEntity entity);

        Task AddRange(IEnumerable<TEntity> entities);

        Task DeleteMany(Expression<Func<TEntity, bool>> predicate);

        Task DeleteOne(Expression<Func<TEntity, bool>> predicate);

        Task RemoveRange(IEnumerable<TEntity> entities);

        Task Update(TEntity item);

        Task Delete(TEntity entity);
    }
}
