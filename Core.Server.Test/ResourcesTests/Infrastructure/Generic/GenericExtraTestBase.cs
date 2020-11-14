using Core.Server.Shared.Resources;
using Core.Server.Tests.Configuration;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Core.Server.Tests.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;

namespace Core.Server.Tests.ResourceTests
{
    public abstract class GenericExtraTestBase<TCreateResource, TUpdateResource, TResource>
        : GenericTestsBase<TResource>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
    {
        [Dependency]
        public IResourceAlter<TCreateResource, TUpdateResource, TResource> ResourceAlter;

        [Dependency]
        public IUnityContainer UnityContainer;

        [Dependency]
        public TestConfig TestConfig;

        [TestInitialize]
        public override void TestInit()
        {
            var testsUnityContainer = new TestsUnityContainer();
            testsUnityContainer.UnityContainer.BuildUp(this);
            CreatedResource = ResourceCreate.Create().Value;
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }
    }
}
