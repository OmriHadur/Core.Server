using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Unity;
using Core.Server.Injection.Attributes;
using Core.Server.Tests.ResourceTests.Interfaces;

namespace Core.Server.Tests.ResourceTests
{
    [Inject]
    public class ResourceGenericAlterTests<TCreateResource, TUpdateResource, TResource>
        : ResourceGenericTestsBase<TResource>
        , IResourceGenericAlterTests
        where TCreateResource : CreateResource, new()
        where TUpdateResource : UpdateResource
        where TResource : Resource
    {

        [Dependency]
        public IResourceAlter<TCreateResource, TUpdateResource, TResource> resourceAlter;

        [TestMethod]
        public void TestUpdate()
        {
            var response = resourceAlter.Replace();
            AssertOk(response);
        }
    }
}