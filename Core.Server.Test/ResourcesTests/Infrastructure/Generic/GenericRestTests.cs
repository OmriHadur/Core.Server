using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceTests.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Server.Tests.ResourceTests
{
    public class GenericRestTests<TResource>
        : GenericTestsBase<TResource>
        , IResourceGenericRestTests
        where TResource : Resource
    {
        public override void TestInit()
        {
            CreateResource();
        }

        [TestMethod]
        public virtual void TestCreateAddedToList()
        {
            var originalListCount = GetAllExistingCount();
            CreateResource();
            var newListCount = GetAllExistingCount();
            Assert.AreEqual(originalListCount + 1, newListCount);
        }

        [TestMethod]
        public virtual void TestGet()
        {
            var id = ResourcesIdsHolder.GetLast<TResource>();
            var result = ResourceQuery.Get(id);
            AssertNoError(result);
            Validate(CreatedResource, result.Value);
        }

        [TestMethod]
        public virtual void TestGetNotFound()
        {
            var response = ResourceQuery.Get("5fb00552d72114101e33fa47");
            AssertNotFound(response);
        }

        [TestMethod]
        public virtual void TestDelete()
        {
            var response = ResourceCreate.Delete(CreatedResource.Id);
            AssertOk(response);
        }

        [TestMethod]
        public virtual void TestGetNotFoundAfterDelete()
        {
            ResourceCreate.Delete(CreatedResource.Id);
            var response = ResourceQuery.Get(CreatedResource.Id);
            AssertNotFound(response);
        }

        [TestMethod]
        public virtual void TestDeleteNotFoundAfterDelete()
        {
            ResourceCreate.Delete(CreatedResource.Id);
            var response = ResourceCreate.Delete(CreatedResource.Id);
            AssertNotFound(response);
        }
    }
}