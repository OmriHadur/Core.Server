using Core.Server.Shared.Resources;
using Core.Server.Test.Configuration;
using Core.Server.Test.ResourceCreators.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;

namespace Core.Server.Test.ResourceTests
{
    public abstract class GenericExtraTestBase<TAlterResource, TResource>
        : GenericTestsBase<TResource>
        where TResource : Resource
    {
        [Dependency]
        public IResourceAlter<TAlterResource, TResource> ResourceAlter;

        [Dependency]
        public IUnityContainer UnityContainer;

        [Dependency]
        public TestConfig TestConfig;

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }
    }
}