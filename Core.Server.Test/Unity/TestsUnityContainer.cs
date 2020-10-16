using Core.Server.Client.Clients;
using Core.Server.Common.Config;
using Core.Server.Injection.Reflaction;
using Core.Server.Injection.Unity;
using Core.Server.Tests.Configuration;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Core.Server.Tests.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity;

namespace Core.Server.Tests.Unity
{
    public class TestsUnityContainer
    {
        public IUnityContainer UnityContainer { get; private set; }

        public TestsUnityContainer()
        {
            UnityContainer = new UnityContainer();
            var config = GetTestConfig();
            UnityContainer.RegisterInstance(typeof(TestConfig), config);
            var reflactionHelper = new ReflactionHelper(config.Assemblies);
            var unityContainerBuilder = new UnityContainerBuilder(UnityContainer, reflactionHelper);
            unityContainerBuilder.ConfigureContainer();
        }

        public TestConfig GetTestConfig()
        {
            var config = new ConfigurationBuilder()
                    .AddJsonFile(".\\appsettings.json")
                    .Build().Get<TestConfig>();
            return config;
        }
    }
}
