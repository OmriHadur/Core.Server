using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Server.Shared.Resources;

namespace Core.Server.Tests.ResourceTests
{
    public class ResourceGenericTests<TCreateResource, TUpdateResource, TResource> :
        ResourceGenericTestsBase<TResource>
        where TCreateResource : CreateResource
        where TUpdateResource: UpdateResource
        where TResource : Resource
    {

        [TestMethod]
        public virtual void TestCreated()
        {
            Assert.IsNotNull(CreatedResource);
        }

        [TestMethod]
        public virtual void TestDelete()
        {
            var originalListCount = GetAllExistingCount();
            ResourceCreate.Delete(CreatedResource.Id);
            var deletedListCount = GetAllExistingCount();
            Assert.AreEqual(originalListCount - 1, deletedListCount);
        }

        [TestMethod]
        public void TestGetNotFoundAfterDelete()
        {
            ResourceCreate.Delete(CreatedResource.Id);
            var response = ResourceQuery.Get(CreatedResource.Id);
            AssertNotFound(response);
        }

        [TestMethod]
        public virtual void TestDeleteNotFoundDelete()
        {
            ResourceCreate.Delete(CreatedResource.Id);
            var response = ResourceCreate.Delete(CreatedResource.Id);
            AssertNotFound(response);
        }
    }
}
