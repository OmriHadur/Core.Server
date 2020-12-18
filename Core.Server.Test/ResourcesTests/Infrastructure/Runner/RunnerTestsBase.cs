using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using Core.Server.Test.Unity;
using Core.Server.Injection.Interfaces;
using System.Collections.Generic;
using Core.Server.Test.ResourceTests.Interfaces;
using System.Linq;
using System;
using Core.Server.Injection.Unity;

namespace Core.Server.Test.ResourceTests
{
    public class RunnerTestsBase<TResourceGenericTests>
        where TResourceGenericTests : IResourceGenericTests
    {
        protected IUnityContainer UnityContainer;
        protected IReflactionHelper ReflactionHelper;

        protected IEnumerable<TResourceGenericTests> TestsForResource;

        [TestInitialize]
        public void TestInit()
        {
            var testsUnityContainer = new TestsUnityContainer();
            UnityContainer = testsUnityContainer.UnityContainer;
            ReflactionHelper = testsUnityContainer.ReflactionHelper;
            TestsForResource = GetTestsForResource();
        }

        [TestCleanup]
        public void Cleanup()
        {
            foreach (var test in TestsForResource)
                test.Cleanup();
        }

        public void RunTestForAllResources(Action<TResourceGenericTests> testRun)
        {
            foreach (var testForResource in TestsForResource)
            {
                testForResource.TestInit();
                testRun(testForResource);
                testForResource.Cleanup();
            }
        }

        private IEnumerable<TResourceGenericTests> GetTestsForResource()
        {
            var testsForResource = new List<TResourceGenericTests>();
            var boundles = GetBoundles();

            var type = ReflactionHelper.GetClassForInterface<TResourceGenericTests>();
            var overideTests = ReflactionHelper.GetDrivenTypesOf(type);

            foreach (var boundle in boundles)
            {
                var genericType = ReflactionHelper.FillGenericType(type, boundle);
                var hasoveride = overideTests.Any(test => test.BaseType == genericType);
                if (!hasoveride)
                {
                    var obj = UnityContainer.Resolve(genericType);
                    testsForResource.Add((TResourceGenericTests)obj);
                }
            }

            foreach (var overideTest in overideTests)
            {
                var obj = UnityContainer.Resolve(overideTest);
                testsForResource.Add((TResourceGenericTests)obj);
            }
            return testsForResource;
        }

        protected virtual List<ResourceBoundle> GetBoundles()
        {
            return ReflactionHelper.GetResourcesBoundles().ToList();
        }
    }
}
