using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceTests.Interfaces;
using Core.Server.Injection.Attributes;

namespace Core.Server.Tests.ResourceTests
{
    [Inject]
    public class ResourceGenericQueryTests<TResource>
        : ResourceTestsBase<TResource>
        , IResourceGenericQueryTests
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
            var result = ResourceQuery.Get(id);
            AssertOk(result);
            Validate(CreatedResource, result.Value);
        }

        [TestMethod]
        public virtual void TestGetNotFound()
        {
            var response = ResourceQuery.Get(RandomId);
            AssertNotFound(response);
        }
    }
}