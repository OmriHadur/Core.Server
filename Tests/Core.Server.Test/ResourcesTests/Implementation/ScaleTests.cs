using Core.Server.Shared.Resources;
using Core.Server.Test.ResourceCreators.Interfaces;
using Core.Server.Test.ResourceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Test.ResourcesTests.Implementation
{
    [TestClass]
    public class ScaleTests
         : GenericExtraTestBase<ExampleAlterResource, ExampleResource>
    {
        protected const int SCALE = 100;

        [Dependency]
        public IResourceBatch<ExampleAlterResource, ExampleResource> ResourceBatch;

        [TestMethod]
        public void TestCreate()
        {
            for (int i = 0; i < SCALE; i++)
            {
                CreateResource();
                Assert.IsNotNull(CreatedResource);
            }
        }

        [TestMethod]
        public void TestCreatePerralel()
        {
            CreateResource();
            Parallel.For(0, SCALE, i => CreateResource());
        }

        [TestMethod]
        public void TestBatchCreate()
        {
            for (int i = 0; i < SCALE / 100; i++)
                ResourceBatch.Create(100);
        }

        [TestMethod]
        public void TestCreateAndGet()
        {
            for (int i = 0; i < SCALE; i++)
            {
                CreateResource();
                ResourceLookup.Get(CreatedResource.Id);
            }
        }
    }
}
