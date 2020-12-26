using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Server.Shared.Resources;

namespace Core.Server.Common.Applications
{
    public interface IAlterApplication<TAlterResource, TResource> :
        IBaseApplication
        where TResource : Resource
    {
        Task<ActionResult<TResource>> Create(TAlterResource resource);

        Task<ActionResult<TResource>> Replace(string id, TAlterResource resource);

        Task<ActionResult<TResource>> Update(string id, TAlterResource resource);

        Task<ActionResult> Delete(string id);

        Task<ActionResult> DeleteAll();
    }
}