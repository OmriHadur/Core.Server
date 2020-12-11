using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Server.Tests.ResourceTests.Interfaces;
using Core.Server.Injection.Unity;
using System.Collections.Generic;
using System.Linq;

namespace Core.Server.Tests.ResourceTests
{
    [TestClass]
    public class RunnerChildTests
        : RunnerTestsBase<IResourceGenericChildTests>
        , IResourceGenericChildTests
    {
        [TestMethod]
        public void TestCreate()
        {
            RunTestForAllResources(t => t.TestCreate());
        }

        [TestMethod]
        public void TestDelete()
        {
            RunTestForAllResources(t => t.TestDelete());
        }

        [TestMethod]
        public void TestReplace()
        {
            RunTestForAllResources(t => t.TestReplace());
        }

        [TestMethod]
        public void TestUpdate()
        {
            RunTestForAllResources(t => t.TestUpdate());
        }

        protected override List<ResourceBoundle> GetBoundles()
        {
            return ReflactionHelper.GetChildResourcesBoundles().ToList();
        }
    }
}
