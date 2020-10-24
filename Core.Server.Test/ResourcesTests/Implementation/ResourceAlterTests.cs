using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Server.Tests.ResourceTests.Interfaces;

namespace Core.Server.Tests.ResourceTests
{
    [TestClass]
    public class ResourceAlterTests
        : ResourceTestsBase<IResourceGenericAlterTests>
        , IResourceGenericAlterTests
    {
        [TestMethod]
        public virtual void TestReplace()
        {
            RunTest(t => t.TestReplace());
        }

        [TestMethod]
        public void TestReplaceCreated()
        {
            RunTest(t => t.TestReplaceCreated());
        }

        [TestMethod]
        public virtual void TestUpdate()
        {
            RunTest(t => t.TestUpdate());
        }

        [TestMethod]
        public void TestGetAfterUpdate()
        {
            RunTest(t => t.TestGetAfterUpdate());
        }
    }
}
