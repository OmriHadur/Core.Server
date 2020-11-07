using Core.Server.Common.Applications;
using Core.Server.Common.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity;
using Core.Server.Shared.Resources;
using Core.Server.Common.Mappers;
using System.Linq;
using Core.Server.Injection.Attributes;

namespace Core.Server.Application
{
    [Inject]
    public class LookupApplication<TResource, TEntity>
        : BaseApplication<TEntity>,
          ILookupApplication<TResource>
        where TResource : Resource
        where TEntity : Entity
    {

        [Dependency]
        public IResourceMapper<TResource, TEntity> ResourceMapper;

        public virtual async Task<ActionResult<IEnumerable<TResource>>> GetAll()
        {
            var entities = await LookupRepository.Get();
            var response = await ResourceMapper.Map(entities);
            return response.ToList();
        }

        public virtual async Task<ActionResult<IEnumerable<TResource>>> GetByIds(string[] ids)
        {
            var entities = await LookupRepository.Get(ids);
            var notFoundId = ids.FirstOrDefault(id => !entities.Any(e => e.Id == id));
            if (notFoundId != null)
                return NotFound(notFoundId);
            var response = await ResourceMapper.Map(entities);
            return response.ToList();
        }

        public virtual async Task<ActionResult<TResource>> GetById(string id)
        {
            var entity = await LookupRepository.Get(id);
            if (entity == null)
                return NotFound(id);
            return await ResourceMapper.Map(entity);
        }

        public virtual async Task<ActionResult> Exists(string id)
        {
            return await LookupRepository.Exists(id) ?
                Ok() :
                NotFound(id);
        }

        public virtual async Task<ActionResult> Any()
        {
            return await LookupRepository.Any() ?
                Ok() :
                NotFound();
        }
    }
}