using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreators.Interfaces;
using System.Collections.Generic;

namespace Core.Server.Test.ResourcesCreators.Infrastructure
{
    public class ResourceQuery<TResource>
        : ResourceHandling<IQueryClient<TResource>, TResource>
        , IResourceQuery<TResource>
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
