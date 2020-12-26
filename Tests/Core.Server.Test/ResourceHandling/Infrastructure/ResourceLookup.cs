using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using Core.Server.Test.ResourceCreators.Interfaces;
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

        public ActionResult<IEnumerable<TResource>> Get(bool filter)
        {
            var result = Client.Get().Result;
            if (result.IsFail)
                return result;
            if (filter)
                return new OkResultWithObject<IEnumerable<TResource>>(Filter(result.Value));
            else
                return result;
        }

        public IEnumerable<TResource> Get(string[] ids)
        {
            return Client.Get(ids).Result.Value;
        }
    }
}
