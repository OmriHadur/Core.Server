using Core.Server.Shared.Query;
using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Server.Common.Applications
{
    public interface IQueryApplication<TResource>
        : IBaseApplication
        where TResource : Resource
    {
        Task<ActionResult<IEnumerable<TResource>>> Query(QueryResource queryResource);
    }
}