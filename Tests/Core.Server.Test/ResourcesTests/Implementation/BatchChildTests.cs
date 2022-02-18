using Core.Server.Test.ResourceCreators.Interfaces;
using Core.Server.Test.ResourceTests;
using Example.Server.Shared.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Unity;

namespace Core.Server.Test.ResourcesTests.Implementation
{
    [TestClass]
    public class BatchChildTests
         : GenericExtraTestBase<ExampleChildAlterResource, ExampleResource>
    {
        [Dependency]
        public IChildResourceBatch<ExampleChildAlterResource, ExampleResource, ExampleChildResource> ResourceBatch;

        [TestMethod]
        public void TestBatchCreate()
        {
            var result = ResourceBatch.Create(5);
            AssertOk(result);
            var parents = ResourceLookup.Get().Value;
            var childrenAmount = parents.Sum(p => p.ChildResources.Length);
            Assert.AreEqual(5, childrenAmount);
        }
    }
}
