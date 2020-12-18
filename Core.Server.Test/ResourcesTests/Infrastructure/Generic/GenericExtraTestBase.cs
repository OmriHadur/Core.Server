using Core.Server.Shared.Resources;
using Core.Server.Test.Configuration;
using Core.Server.Test.ResourceCreators.Interfaces;
using Core.Server.Test.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;

namespace Core.Server.Test.ResourceTests
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
            testsUnityContainer.UnityContainer.BuildUp(GetThis().GetType(), GetThis());
        }

        protected virtual object GetThis() => this;

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }
    }
}