using Core.Server.Common.Applications;
using Core.Server.Common.Entities;
using Core.Server.Injection.Attributes;
using Core.Server.Shared.Resources;
using Core.Server.Shared.Resources.Users;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application.Logging
{
    [Inject(2)]
    public class LoggingLookupApplication<TResource, TEntity>
        : ILookupApplication<TResource>
        where TResource : Resource
        where TEntity : Entity
    {
        [Dependency]
        public LookupApplication<TResource, TEntity> LookupApplication;

        public UserResource CurrentUser
        {
            get => LookupApplication.CurrentUser;
            set { LookupApplication.CurrentUser = value; }
        }

        public Task<ActionResult> Any()
        {
            return LookupApplication.Any();
        }

        public Task<ActionResult> Exists(string id)
        {
            return LookupApplication.Exists(id);
        }

        public Task<ActionResult<IEnumerable<TResource>>> GetAll()
        {
            return LookupApplication.GetAll();
        }

        public Task<ActionResult<TResource>> GetById(string id)
        {
            return LookupApplication.GetById(id);
        }

        public Task<ActionResult<IEnumerable<TResource>>> GetByIds(string[] ids)
        {
            return LookupApplication.GetByIds(ids);
        }
    }
}
