using Core.Server.Common.Applications;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity;
using Core.Server.Shared.Resources;
using System.Linq;
using System;

namespace Core.Server.Application
{
    public class BatchApplication<TCreateResource, TUpdateResource, TResource, TEntity>
        : BaseApplication,
        IBatchApplication<TCreateResource, TUpdateResource, TResource>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
        where TEntity : Entity, new()
    {
        [Dependency]
        public IBatchRepository<TEntity> BatchRepository { get; set; }

        public async Task<ActionResult<IEnumerable<TResource>>> BatchCreate(TCreateResource[] resources)
        {
            foreach (var resource in resources)
            {
                var validation = await Validate(resource);
                if (!(validation is OkResult))
                    return validation;
            }

            var entities = resources.Select(r => GetNewTEntity(r));
            await AddEntites(entities);
            return await MapMany(entities);
        }

        private Task AddEntites(IEnumerable<TEntity> entities)
        {
            return BatchRepository.AddMany(entities);
        }

        public async Task<ActionResult<IEnumerable<TResource>>> BatchGet(string[] ids)
        {
            var entities = await BatchRepository.FindAll(e => ids.Contains(e.Id));

            var notFoundId = ids.FirstOrDefault(id => !entities.Any(e => e.Id == id));
            if (notFoundId != null)
                return NotFound(notFoundId);

            return await MapMany(entities);
        }
        public Task<ActionResult<IEnumerable<TResource>>> BatchUpdate(TUpdateResource[] resources)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult> BatchDelete(string[] ids)
        {
            await BatchRepository.DeleteMany(ids);
            return Ok();
        }
    }
}