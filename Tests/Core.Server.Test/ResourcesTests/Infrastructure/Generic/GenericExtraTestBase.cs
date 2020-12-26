using Core.Server.Shared.Resources;
using Core.Server.Test.Configuration;
using Core.Server.Test.ResourceCreators.Interfaces;
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

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }
    }
}