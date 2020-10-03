using MongoDB.Driver;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;

namespace Core.Server.Persistence.Repositories
{
    public class BatchRepository<TEntity> :
        RestRepository<TEntity>,
        IBatchRepository<TEntity>
        where TEntity : Entity
    {
        public async Task AddMany(IEnumerable<TEntity> entities)
        {
            await Entities.InsertManyAsync(entities);
        }

        public async Task DeleteMany(string[] ids)
        {
             await Entities.DeleteManyAsync(e => ids.Contains(e.Id));
        }
    }
}
