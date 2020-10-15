﻿using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Core.Server.Tests.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;

namespace Core.Server.Test.ResourcesCreators.Infrastructure
{
    public class ResourceHandlingBase<TClient, TResource>
        where TClient : IClientBase
        where TResource : Resource
    {
        private TClient _client;

        [Dependency]
        public IResourcesIdsHolder ResourceIdsHolder;

        [Dependency]
        public ITokenHandler TokenHandler;

        [Dependency]
        public IConfigHandler ConfigHandler;

        [Dependency]
        public TClient Client
        {
            get
            {
                if (_client.ServerUrl == null)
                    _client.ServerUrl = ConfigHandler.Config.ServerUrl;
                if (_client.Token == null)
                {
                    TokenHandler.OnTokenChange += (s, t) => _client.Token = t;
                    _client.Token = TokenHandler.Token;
                }

                return _client;
            }
            set { _client = value; }
        }
    }
}
