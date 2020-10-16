using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreators.Interfaces;

namespace Core.Server.Tests.ResourceTests
{
    public abstract class ResourceAlterTests<TCreateResource, TUpdateResource, TResource> :
        ResourceTestsBase<TResource>
        where TCreateResource : CreateResource, new()
        where TUpdateResource: UpdateResource
        where TResource : Resource
    {

        private readonly IResourceAlter<TCreateResource, TUpdateResource, TResource> resourceAlter;

        public ResourceAlterTests()
        {
            resourceAlter = UnityContainer.Resolve<IResourceAlter<TCreateResource, TUpdateResource, TResource>>();
        }

        [TestMethod]
        public void Update()
        {
            var response = resourceAlter.Replace();
            AssertOk(response);
        }
    }
}
