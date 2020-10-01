using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using Core.Server.Shared.Resources.Users;
using System;
using System.Collections.Generic;

namespace Core.Server.Tests.ResourceCreators.Interfaces
{
    public interface IResourcesHolder
    {
        IEnumerable<TResource> GetAllExisting<TResource>()
            where TResource : Resource;

        ActionResult<TResource> GetLastOrCreate<TResource>()
            where TResource : Resource;

        ActionResult<TResource> Get<TResource>(string id)
            where TResource : Resource;

        string GetResourceId<TResource>()
            where TResource : Resource;

        IEnumerable<string> GetResourceIds<TResource>()
            where TResource : Resource;

        ActionResult<TResource> Create<TResource>()
            where TResource : Resource;

        ActionResult<TResource> Create<TCreateResource, TResource>(TCreateResource createResource)
            where TCreateResource : CreateResource, new()
            where TResource : Resource;

        ActionResult<TResource> EditAndCreate<TCreateResource, TResource>(Action<TCreateResource> edit)
            where TCreateResource : CreateResource, new()
            where TResource : Resource;

        ActionResult Delete<TResource>(string id)
            where TResource : Resource;

        void DeleteAll<TResource>()
            where TResource : Resource;

        void DeleteAll();

        UserResource GetLoggedUser();
    }
}