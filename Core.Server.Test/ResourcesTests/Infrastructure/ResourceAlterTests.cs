using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Unity;
using Core.Server.Injection.Attributes;

namespace Core.Server.Tests.ResourceTests
{
    [InjectBoundleTest]
    public class ResourceAlterTests<TCreateResource, TUpdateResource, TResource> :
        ResourceTestsBase<TResource>
        where TCreateResource : CreateResource, new()
        where TUpdateResource: UpdateResource
        where TResource : Resource
    {

        [Dependency]
        public IResourceAlter<TCreateResource, TUpdateResource, TResource> resourceAlter;

        [TestMethod]
        public void Update()
        {
            var response = resourceAlter.Replace();
            AssertOk(response);
        }
    }
}
