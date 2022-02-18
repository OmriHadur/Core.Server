using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Server.Common.Applications
{
    public interface ILookupApplication<TResource>
        : IBaseApplication
        where TResource : Resource
    {
        Task<ActionResult<IEnumerable<TResource>>> GetAll();

        Task<ActionResult<IEnumerable<TResource>>> GetByIds(string[] ids);

        Task<ActionResult<TResource>> GetById(string id);

        Task<ActionResult> Exists(string id);

        Task<ActionResult> Any();
    }
}