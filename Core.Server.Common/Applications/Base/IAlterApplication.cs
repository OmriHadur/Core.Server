using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Server.Shared.Resources;

namespace Core.Server.Common.Applications
{
    public interface IAlterApplication<TCreateResource, TUpdateResource, TResource>:
        IQueryApplication<TResource>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
    {
        Task<ActionResult<TResource>> Create(TCreateResource resource);

        Task<ActionResult<TResource>> CreateOrUpdate(string id,TCreateResource resource);

        Task<ActionResult<TResource>> Update(string id, TUpdateResource resource);

        Task<ActionResult> Delete(string id);
    }
}