using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Server.Shared.Resources;
using System.Linq;

namespace Core.Server.Tests.ResourceTests
{
    public class ResourceQueryTests<TResource> :
        ResourceTestsBase< TResource>
        where TResource : Resource
    {
        [TestMethod]
        public virtual void TestList()
        {
            Assert.AreEqual(1, GetAllExistingCount());
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
            var resourceResult = ResourceQuery.Get(id);
            Assert.IsNotNull(resourceResult.Value);
            Validate(CreatedResource, resourceResult.Value);
        }

        [TestMethod]
        public virtual void TestGetNotFound()
        {
            var response = ResourceQuery.Get(RandomId);
            AssertNotFound(response);
        }
    }
}
