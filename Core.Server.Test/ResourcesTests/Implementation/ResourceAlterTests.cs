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
        public virtual void TestUpdate()
        {
            RunTest(t => t.TestUpdate());
        }
    }
}
