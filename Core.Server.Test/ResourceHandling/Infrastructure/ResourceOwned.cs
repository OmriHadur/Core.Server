using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreators.Interfaces;
using System.Collections.Generic;

namespace Core.Server.Test.ResourcesCreators.Infrastructure
{
    [Inject]
    public class ResourceOwned<TResource>
        : ResourceHandling<IOwnedClient<TResource>, TResource>
        , IResourceOwned<TResource>
        where TResource : Resource
    {
        public ActionResult Any()
        {
            return Client.Any().Result;
        }

        public ActionResult<IEnumerable<TResource>> GetAll()
        {
            return Filter(Client.GetAll().Result);
        }

        public ActionResult Reassigen(string resourceId, string userEmail)
        {
            var reassginResource = new ReassginResource()
            {
                ResourceId = resourceId,
                UserEmail = userEmail
            };
            return Client.Reassign(reassginResource).Result;
        }
    }
}
