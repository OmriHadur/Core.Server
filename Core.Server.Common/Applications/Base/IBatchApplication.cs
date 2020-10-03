using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Server.Shared.Resources;
using Core.Server.Shared.Query;
using Core.Server.Shared.Resources.Users;

namespace Core.Server.Common.Applications
{
    public interface IBatchApplication<TCreateResource, TUpdateResource, TResource>
        :IRestApplication<TCreateResource, TUpdateResource, TResource>
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