using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Server.Common.Applications
{
    public interface IBatchApplication<TAlterResource, TResource>
        : IBaseApplication
        where TResource : Resource
    {
        Task<ActionResult<IEnumerable<TResource>>> BatchCreate(TAlterResource[] resources);

        Task<ActionResult<IEnumerable<TResource>>> BatchUpdate(TAlterResource[] resources);

        Task<ActionResult<IEnumerable<string>>> BatchDelete(string[] ids);

    }
}