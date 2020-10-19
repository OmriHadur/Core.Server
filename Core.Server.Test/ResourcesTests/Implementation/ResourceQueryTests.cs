using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Server.Tests.ResourceTests.Interfaces;

namespace Core.Server.Tests.ResourceTests
{
    [TestClass]
    public class ResourceQueryTests
        : ResourceTestsBase<IResourceGenericRestTests>
        , IResourceGenericRestTests
    {
        [TestMethod]
        public void TestCreateAddedToList()
        {
            RunTest(t => t.TestCreateAddedToList());
        }

        [TestMethod]
        public void TestDelete()
        {
            RunTest(t => t.TestDelete());
        }

        [TestMethod]
        public void TestDeleteNotFoundAfterDelete()
        {
            RunTest(t => t.TestDeleteNotFoundAfterDelete());
        }

        [TestMethod]
        public virtual void TestGet()
        {
            RunTest(t => t.TestGet());
        }

        [TestMethod]
        public virtual void TestGetNotFound()
        {
            RunTest(t => t.TestGetNotFound());
        }

        [TestMethod]
        public virtual void TestGetNotFoundAfterDelete()
        {
            RunTest(t => t.TestGetNotFoundAfterDelete());
        }
    }
}
