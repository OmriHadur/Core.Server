using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreators.Interfaces;
using System.Collections.Generic;

namespace Core.Server.Test.ResourcesCreators.Infrastructure
{
    [Inject]
    public class ResourceLookup<TResource>
        : ResourceHandling<ILookupClient<TResource>, TResource>
        , IResourceLookup<TResource>
        where TResource : Resource
    {
        public ActionResult<TResource> Get(string id)
        {
            return Client.Get(id).Result;
        }

        public IEnumerable<TResource> Get()
        {
            return Client.Get().Result.Value;
        }

        public IEnumerable<TResource> Get(string[] ids)
        {
            return Client.Get(ids).Result.Value;
        }
    }
}
