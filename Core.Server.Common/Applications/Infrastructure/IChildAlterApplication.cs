using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core.Server.Common.Applications
{
    public interface IChildAlterApplication<TCreateResource, TUpdateResource, TResource> :
        IBaseApplication
        where TCreateResource : ChildCreateResource
        where TUpdateResource : ChildUpdateResource
        where TResource : Resource
    {
        Task<ActionResult<TResource>> Create(TCreateResource resource);

        Task<ActionResult<TResource>> Replace(string id, TCreateResource resource);

        Task<ActionResult<TResource>> Update(string id, TUpdateResource resource);

        Task<ActionResult<TResource>> Delete(string parentId, string id);

        Task<ActionResult<TResource>> DeleteAll(string parentId);
    }
}