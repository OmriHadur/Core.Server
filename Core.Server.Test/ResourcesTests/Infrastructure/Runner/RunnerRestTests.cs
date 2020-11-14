using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Server.Tests.ResourceTests.Interfaces;

namespace Core.Server.Tests.ResourceTests
{
    [TestClass]
    public class RunnerRestTests
        : RunnerTestsBase<IResourceGenericRestTests>
        , IResourceGenericRestTests
    {
        [TestMethod]
        public void TestCreateAddedToList()
        {
            RunTestForAllResources(t => t.TestCreateAddedToList());
        }

        [TestMethod]
        public void TestDelete()
        {
            RunTestForAllResources(t => t.TestDelete());
        }

        [TestMethod]
        public void TestDeleteNotFoundAfterDelete()
        {
            RunTestForAllResources(t => t.TestDeleteNotFoundAfterDelete());
        }

        [TestMethod]
        public virtual void TestGet()
        {
            RunTestForAllResources(t => t.TestGet());
        }

        [TestMethod]
        public virtual void TestGetNotFound()
        {
            RunTestForAllResources(t => t.TestGetNotFound());
        }

        [TestMethod]
        public virtual void TestGetNotFoundAfterDelete()
        {
            RunTestForAllResources(t => t.TestGetNotFoundAfterDelete());
        }
    }
}
