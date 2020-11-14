using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Server.Tests.ResourceTests.Interfaces;

namespace Core.Server.Tests.ResourceTests
{
    [TestClass]
    public class RunnerAlterTests
        : RunnerTestsBase<IResourceGenericAlterTests>
        , IResourceGenericAlterTests
    {
        [TestMethod]
        public virtual void TestReplace()
        {
            RunTestForAllResources(t => t.TestReplace());
        }

        [TestMethod]
        public void TestReplaceCreated()
        {
            RunTestForAllResources(t => t.TestReplaceCreated());
        }

        [TestMethod]
        public virtual void TestUpdate()
        {
            RunTestForAllResources(t => t.TestUpdate());
        }

        [TestMethod]
        public void TestGetAfterUpdate()
        {
            RunTestForAllResources(t => t.TestGetAfterUpdate());
        }
    }
}
