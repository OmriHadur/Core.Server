using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Server.Shared.Resources;

namespace Core.Server.Tests.ResourceTests
{
    public class ResourceTests<TCreateResource, TUpdateResource, TResource> :
        ResourceTestsBase<TResource>
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
            ResourcesHolder.Delete<TResource>(CreatedResource.Id);
            var response = ResourcesHolder.Get<TResource>(CreatedResource.Id);
            AssertNotFound(response);
        }

        [TestMethod]
        public virtual void TestDeleteNotFoundDelete()
        {
            ResourcesHolder.Delete<TResource>(CreatedResource.Id);
            var response = ResourcesHolder.Delete<TResource>(CreatedResource.Id);
            AssertNotFound(response);
        }
    }
}
