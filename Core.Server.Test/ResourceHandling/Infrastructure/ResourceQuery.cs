using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Query;
using Core.Server.Shared.Resources;
using Core.Server.Test.ResourceCreators.Interfaces;
using System.Collections.Generic;
using Unity;

namespace Core.Server.Test.ResourcesCreators.Infrastructure
{
    [Inject]
    public class ResourceQuery<TResource>
        : ResourceHandling<IQueryClient<TResource>, TResource>
        , IResourceQuery<TResource>
        where TResource : Resource
    {
        public ActionResult<IEnumerable<TResource>> Query(QueryResource queryResource)
        {
            return Filter(Client.Query(queryResource).Result);
        }
    }
}