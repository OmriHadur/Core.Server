using Core.Server.Common.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Server.Common.Repositories
{
    public interface IAlterRepository<TEntity>
        : IBaseRepository
        where TEntity : Entity
    {
        Task Add(TEntity entity);

        Task DeleteMany(Expression<Func<TEntity, bool>> predicate);

        Task DeleteOne(Expression<Func<TEntity, bool>> predicate);

        Task Replace(TEntity entity);

        Task Update(TEntity entity);

        Task Delete(TEntity entity);
    }
}
