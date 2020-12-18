using Core.Server.Client.Clients;
using Core.Server.Common.Config;
using Core.Server.Injection.Interfaces;
using Core.Server.Injection.Reflaction;
using Core.Server.Injection.Unity;
using Core.Server.Test.Configuration;
using Core.Server.Test.ResourceCreators.Interfaces;
using Core.Server.Test.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity;

namespace Core.Server.Test.Unity
{
    public class TestsUnityContainer
    {
        public IUnityContainer UnityContainer { get; private set; }
        public IReflactionHelper ReflactionHelper { get; private set; }

        public TestsUnityContainer()
        {
            UnityContainer = new UnityContainer();
            var config = GetTestConfig();
            UnityContainer.RegisterInstance(typeof(TestConfig), config);
            ReflactionHelper = new ReflactionHelper(config.Assemblies);
            UnityContainer.RegisterInstance(ReflactionHelper);
            var unityContainerBuilder = new UnityContainerBuilder(UnityContainer, ReflactionHelper);
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
