using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core.Server.Common.Applications
{
    public interface IChildAlterApplication<TAlterResource, TResource> :
        IBaseApplication
        where TAlterResource : ChildAlterResource
        where TResource : Resource
    {
        Task<ActionResult<TResource>> Create(TAlterResource resource);

        Task<ActionResult<TResource>> Replace(string id, TAlterResource resource);

        Task<ActionResult<TResource>> Update(string id, TAlterResource resource);

        Task<ActionResult<TResource>> Delete(string parentId, string id);

        Task<ActionResult<TResource>> DeleteAll(string parentId);
    }
}