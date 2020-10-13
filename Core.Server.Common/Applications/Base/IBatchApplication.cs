using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Server.Shared.Resources;

namespace Core.Server.Common.Applications
{
    public interface IBatchApplication<TCreateResource, TUpdateResource, TResource>
        : IBaseApplication
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
    {
        Task<ActionResult<IEnumerable<TResource>>> BatchGet(string[] ids);
        Task<ActionResult<IEnumerable<TResource>>> BatchCreate(TCreateResource[] resources);
        Task<ActionResult<IEnumerable<TResource>>> BatchUpdate(TUpdateResource[] resources);
        Task<ActionResult> BatchDelete(string[] ids);

    }
}