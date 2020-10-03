using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Server.Shared.Resources;
using Core.Server.Shared.Query;

namespace Core.Server.Common.Applications
{
    public interface IQueryApplication<TResource>
        : IBaseApplication
        where TResource : Resource
    {
        Task<ActionResult<IEnumerable<TResource>>> Get();

        Task<ActionResult<IEnumerable<TResource>>> Query(QueryResource query);

        Task<ActionResult<TResource>> Get(string id);

        Task<ActionResult> Exists(string id);
    }
}