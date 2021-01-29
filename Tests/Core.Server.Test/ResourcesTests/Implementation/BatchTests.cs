using Core.Server.Test.ResourceCreators.Interfaces;
using Core.Server.Test.ResourceTests;
using Example.Server.Shared.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Unity;

namespace Core.Server.Test.ResourcesTests.Implementation
{
    [TestClass]
    public class BatchTests
         : GenericExtraTestBase<ExampleAlterResource, ExampleResource>
    {
        [Dependency]
        public IResourceBatch<ExampleAlterResource, ExampleResource> ResourceBatch;

        [TestMethod]
        public void TestBatchCreate()
        {
            var response = ResourceBatch.Create(5);

            var amountAfter = ResourceLookup.Get().Value.Count();
            Assert.AreEqual(5, amountAfter);
            AssertOk(response);
        }

        [TestMethod]
        public void TestBatchDelete()
        {
            var createResponse = ResourceBatch.Create(5);
            var deleteResponse = ResourceBatch.Delete(createResponse.Value.Select(r => r.Id));

            var amountAfter = ResourceLookup.Get().Value.Count();
            Assert.AreEqual(0, amountAfter);
            AssertOk(deleteResponse);
        }

        [TestMethod]
        public void TestBatchDeleteOk()
        {
            CreateResource();
            var deleteResponse = ResourceBatch.Delete(new string[] { CreatedResource.Id, GetRandomId() });
            AssertValidationError(deleteResponse);
        }
    }
}
