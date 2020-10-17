using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Server.Tests.ResourceTests.Interfaces;

namespace Core.Server.Tests.ResourceTests
{
    [TestClass]
    public class ResourceQueryTests
        : ResourceTestsBase<IResourceGenericQueryTests>
        , IResourceGenericQueryTests
    {
        [TestMethod]
        public virtual void TestList()
        {
            RunTest(t => t.TestList());
        }

        [TestMethod]
        public virtual void TestCreateAddedToList()
        {
            RunTest(t => t.TestCreateAddedToList());
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
    }
}
