using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Server.Common.Applications
{
    public interface IChildBatchApplication<TAlterResource, TResource>
        : IBaseApplication
        where TAlterResource : ChildAlterResource
        where TResource : Resource
    {
        Task<ActionResult<IEnumerable<TResource>>> BatchCreate(TAlterResource[] resources);

        Task<ActionResult<IEnumerable<TResource>>> BatchUpdate(TAlterResource[] resources);

        Task<ActionResult<IEnumerable<string>>> BatchDelete(ChildBatchDeleteResource childBatchDeleteResource);
    }
}