using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using Core.Server.Test.Configuration;
using Core.Server.Test.ResourceCreators.Interfaces;
using Core.Server.Test.Utils;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace Core.Server.Test.ResourcesCreators.Infrastructure
{
    public abstract class ResourceHandling<TClient, TResource>
        where TClient : IClientBase
        where TResource : Resource
    {
        private TClient _client;

        [Dependency]
        public IResourcesIdsHolder ResourceIdsHolder;

        [Dependency]
        public ICurrentUser CurrentUser;

        [Dependency]
        public TestConfig Config;

        [Dependency]
        public TClient Client
        {
            get
            {
                if (_client.ServerUrl == null)
                    _client.ServerUrl = Config.ServerUrl;
                if (string.IsNullOrEmpty(_client.Token))
                {
                    CurrentUser.OnTokenChange += (s, t) => _client.Token = t;
                    _client.Token = CurrentUser.Token;
                }

                return _client;
            }
            set { _client = value; }
        }

        protected ActionResult<IEnumerable<TResource>> Filter(ActionResult<IEnumerable<TResource>> result)
        {
            if (result.IsFail)
                return result;
            var resources = Filter(result.Value);
            return new OkResultWithObject<IEnumerable<TResource>>(resources);
        }

        protected IEnumerable<TResource> Filter(IEnumerable<TResource> result)
        {
            return result.Where(r => ResourceIdsHolder.Contains<TResource>(r.Id));
        }
    }
}
