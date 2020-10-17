using Core.Server.Client.Interfaces;
using Core.Server.Shared.Resources;
using Core.Server.Tests.Configuration;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Core.Server.Tests.Utils;
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
                if (_client.Token == null)
                {
                    CurrentUser.OnTokenChange += (s, t) => _client.Token = t;
                    _client.Token = CurrentUser.Token;
                }

                return _client;
            }
            set { _client = value; }
        }
    }
}
