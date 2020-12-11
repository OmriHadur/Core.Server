using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Core.Server.Tests.ResourceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Unity;

namespace Core.Server.Test.ResourcesTests.Implementation
{
    [TestClass]
    public class BatchChildExmapleTetst
         : GenericExtraTestBase<ExampleChildCreateResource, ExampleChildUpdateResource, ExampleResource>
    {
        [Dependency]
        public IChildResourceBatch<ExampleChildCreateResource, ExampleChildUpdateResource, ExampleResource, ExampleChildResource> ResourceBatch;

        protected override object GetThis() => this;

        [TestMethod]
        public void TestBatchCreate()
        {
            var result = ResourceBatch.Create(5);
            AssertOk(result);
            var parents = ResourceLookup.Get();
            var childrenAmount = parents.Sum(p => p.ChildResources.Length);
            Assert.AreEqual(5, childrenAmount);
        }
    }
}
