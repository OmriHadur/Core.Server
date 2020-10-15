using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreators.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;

namespace Core.Server.Test.ResourcesCreators.Infrastructure
{
    public class QueryResource<TResource>
        : IQueryResource<TResource>
        where TResource : Resource
    {
        [Dependency]
        public IResourcesIdsHolder ResourceIdsHolder;

        [Dependency]
        public IQueryClient<TResource> QueryClient;

        public ActionResult<TResource> Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TResource> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
