using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Core.Server.Tests.ResourceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Unity;

namespace Core.Server.Test.ResourcesTests.Implementation
{
    [TestClass]
    public class BatchExmapleTetst
         : GenericExtraTestBase<ExampleCreateResource, ExampleUpdateResource, ExampleResource>
    {
        [Dependency]
        public IResourceBatch<ExampleCreateResource, ExampleUpdateResource, ExampleResource> ResourceBatch;

        protected override object GetThis() => this;

        [TestMethod]
        public void TestBatchCreate()
        {
            var amountBefore = ResourceLookup.Get().Count();

            var response = ResourceBatch.Create(5);

            var amountAfter = ResourceLookup.Get().Count();
            Assert.AreEqual(amountBefore + 5, amountAfter);
            AssertOk(response);
        }

        [TestMethod]
        public void TestBatchDelete()
        {
            var amountBefore = ResourceLookup.Get().Count();

            var createResponse = ResourceBatch.Create(5);
            var deleteResponse = ResourceBatch.Delete(createResponse.Value.Select(r => r.Id));

            var amountAfter = ResourceLookup.Get().Count();
            Assert.AreEqual(amountBefore, amountAfter);
            AssertOk(deleteResponse);
        }

        [TestMethod]
        public void TestBatchDeleteOk()
        {
            CreateResource();
            var deleteResponse = ResourceBatch.Delete(new string[] { CreatedResource.Id, GetRandomId() });
            AssertOk(deleteResponse);
        }
    }
}
