using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Server.Common.Applications
{
    public interface IChildBatchApplication<TCreateResource, TUpdateResource, TResource>
        : IBaseApplication
        where TCreateResource : ChildCreateResource
        where TUpdateResource : ChildUpdateResource
        where TResource : Resource
    {
        Task<ActionResult<IEnumerable<TResource>>> BatchCreate(TCreateResource[] resources);

        Task<ActionResult<IEnumerable<TResource>>> BatchUpdate(TUpdateResource[] resources);

        Task<ActionResult<IEnumerable<string>>> BatchDelete(ChildBatchDeleteResource childBatchDeleteResource);

    }
}