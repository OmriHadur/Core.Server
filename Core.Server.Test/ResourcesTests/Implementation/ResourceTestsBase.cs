using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using Core.Server.Tests.Unity;
using Core.Server.Injection.Interfaces;
using System.Collections.Generic;
using Core.Server.Tests.ResourceTests.Interfaces;
using System.Linq;
using System;

namespace Core.Server.Tests.ResourceTests
{
    [TestClass]
    public class ResourceTestsBase<TResourceGenericTests>
        where TResourceGenericTests : IResourceGenericTests
    {
        private IUnityContainer UnityContainer;
        private IReflactionHelper ReflactionHelper;

        protected List<TResourceGenericTests> Tests;

        [TestInitialize]
        public void TestInit()
        {
            var testsUnityContainer = new TestsUnityContainer();
            UnityContainer = testsUnityContainer.UnityContainer;
            ReflactionHelper = testsUnityContainer.ReflactionHelper;
            FillTests();
        }

        [TestCleanup]
        public void Cleanup()
        {

        }

        public void RunTest(Action<TResourceGenericTests> testRun)
        {
            foreach (var test in Tests)
            {
                test.TestInit();
                testRun(test);
                test.Cleanup();
            }
        }

        private void FillTests()
        {
            Tests = new List<TResourceGenericTests>();
            var boundles = ReflactionHelper.GetResourcesBoundles().ToList();

            var type = typeof(ResourceGenericQueryTests<>);

            foreach (var boundle in boundles)
            {
                var genericType = ReflactionHelper.FillGenericType(type, boundle);
                var obj = UnityContainer.Resolve(genericType);
                Tests.Add((TResourceGenericTests)obj);
            }
        }
    }
}
