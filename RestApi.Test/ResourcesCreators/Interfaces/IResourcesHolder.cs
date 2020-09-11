using RestApi.Client.Results;
using RestApi.Shared.Resources;
using System;
using System.Collections.Generic;

namespace RestApi.Tests.RestResourcesCreators.Interfaces
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
    }
}