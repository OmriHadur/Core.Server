using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Server.Shared.Resources;
using Core.Server.Shared.Resources.Users;
using Core.Server.Shared.Query;

namespace Core.Server.Common.Applications
{
    public interface IRestApplication<TCreateResource, TUpdateResource, TResource>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
    {
        public UserResource CurrentUser { get; set; }

        Task<ActionResult<IEnumerable<TResource>>> Get();

        Task<ActionResult<IEnumerable<TResource>>> Query(QueryResource query);

        Task<ActionResult<TResource>> Get(string id);

        Task<ActionResult<TResource>> Create(TCreateResource resource);

        Task<ActionResult<TResource>> Update(string id, TUpdateResource resource);

        Task<ActionResult> Delete(string id);

        Task<ActionResult> Exists(string id);
    }
}