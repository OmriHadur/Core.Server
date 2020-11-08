using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Core.Server.Persistence.Repositories;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Server.Persistence.Logging
{
    [Inject(2)]
    public class LoggingAlterRepository<TEntity>
        : LoggingRepository<AlterRepository<TEntity>>,
          IAlterRepository<TEntity>
        where TEntity : Entity
    {
        public Task Add(TEntity entity)
        {
            return LogginCall(() => Repository.Add(entity), entity);
        }

        public Task Delete(string id)
        {
            return LogginCall(() => Repository.Delete(id), id);
        }

        public Task DeleteAll()
        {
            return LogginCall(() => Repository.DeleteAll());
        }

        public Task DeleteMany(Expression<Func<TEntity, bool>> predicate)
        {
            return LogginCall(() => Repository.DeleteMany(predicate), predicate);
        }

        public Task DeleteOne(Expression<Func<TEntity, bool>> predicate)
        {
            return LogginCall(() => Repository.DeleteOne(predicate), predicate);
        }

        public Task Replace(TEntity entity)
        {
            return LogginCall(() => Repository.Replace(entity), entity);
        }

        public Task Update(TEntity entity)
        {
            return LogginCall(() => Repository.Update(entity), entity);
        }
    }
}
