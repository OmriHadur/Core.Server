using Core.Server.Injection.Interfaces;
using Core.Server.Injection.Reflaction;
using Core.Server.Injection.Unity;
using Core.Server.Test.Configuration;
using Microsoft.Extensions.Configuration;
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
            ReflactionHelper = new ReflactionHelper(config.AssembliesPrefixes);
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
