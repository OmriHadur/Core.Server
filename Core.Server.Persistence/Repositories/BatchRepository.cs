﻿using MongoDB.Driver;
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
        BaseRepository<TEntity>,
        IBatchRepository<TEntity>
        where TEntity : Entity
    {
        public async Task AddMany(IEnumerable<TEntity> entities)
        {
            await Collection.InsertManyAsync(entities);
        }

        public async Task DeleteMany(string[] ids)
        {
             await Collection.DeleteManyAsync(e => ids.Contains(e.Id));
        }
    }
}