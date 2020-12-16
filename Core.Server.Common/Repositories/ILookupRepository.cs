using Core.Server.Common.Entities;
using Core.Server.Common.Query;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Server.Common.Repositories
{
    public interface ILookupRepository<TEntity> 
        : IBaseRepository
        where TEntity : Entity
    {
        Task<TEntity> Get(string id);

        Task<IEnumerable<TEntity>> Get(string[] ids);

        Task<IEnumerable<TEntity>> Get();

        Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FindFirst(Expression<Func<TEntity, bool>> predicate);

        Task<bool> Exists(string id);

        Task<string> GetNotFoundId(string[] ids);

        Task<bool> Any();

        Task<bool> Exists(Expression<Func<TEntity, bool>> predicate);
    }
}
