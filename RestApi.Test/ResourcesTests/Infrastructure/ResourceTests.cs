using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestApi.Shared.Resources;

namespace RestApi.Tests.RestRourcesTests
{
    public abstract class ResourceTests<TCreateResource, TResource> :
        ResourceTestsBase<TCreateResource, TResource>
        where TCreateResource : CreateResource, new()
        where TResource : Resource
    {

        [TestMethod]
        public virtual void TestCreateAndRemove()
        {
            Assert.IsNotNull(CreatedResource);
        }

        [TestMethod]
        public virtual void TestList()
        {
            Assert.AreEqual(1, GetAllExistingCount());
        }

        [TestMethod]
        public virtual void TestCreateAddedToList()
        {
            var originalListCount = GetAllExistingCount();
            ResourcesHolder.Create<TResource>();
            var newListCount = GetAllExistingCount();
            Assert.AreEqual(originalListCount + 1, newListCount);
        }

        [TestMethod]
        public virtual void TestGet()
        {
            var resourceResult = ResourcesHolder.GetLastOrCreate<TResource>();
            Assert.IsNotNull(resourceResult.Value);
            Validate(CreatedResource, resourceResult.Value);
        }

        [TestMethod]
        public virtual void TestGetNotFound()
        {
            var response = ResourcesHolder.Get<TResource>(RandomId);
            AssertNotFound(response);
        }

        [TestMethod]
        public virtual void TestUpdate()
        {
            var resourceToUpdate = ResourceCreator.GetRandomCreateResource();
            var updatedItemResult = ResourceCreator.Update(CreatedResource.Id, resourceToUpdate);
            Assert.IsNotNull(updatedItemResult.Value);
            Validate(resourceToUpdate, updatedItemResult.Value);
        }

        [TestMethod]
        public virtual void TestUpdateNotFound()
        {
            var resourceToUpdate = ResourceCreator.GetRandomCreateResource();
            var updatedItemResult = ResourceCreator.Update(RandomId, resourceToUpdate);
            AssertNotFound(updatedItemResult);
        }

        [TestMethod]
        public virtual void TestDelete()
        {
            var originalListCount = GetAllExistingCount();
            ResourcesHolder.Delete<TResource>(CreatedResource.Id);
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
