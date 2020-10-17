using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Server.Shared.Resources;
using Core.Server.Injection.Attributes;
using Unity;
using Core.Server.Tests.Unity;
using Core.Server.Injection.Interfaces;
using System.Collections.Generic;
using Core.Server.Tests.ResourceTests.Interfaces;
using System.Linq;

namespace Core.Server.Tests.ResourceTests
{
    [TestClass]
    public class ResourceQueryTests
    {
        private IUnityContainer UnityContainer;
        private IReflactionHelper ReflactionHelper;
        private List<IResourceGenericQueryTests> tests;


        [TestInitialize]
        public void TestInit()
        {
            var testsUnityContainer = new TestsUnityContainer();
            UnityContainer = testsUnityContainer.UnityContainer;
            ReflactionHelper = testsUnityContainer.ReflactionHelper;
            tests = new List<IResourceGenericQueryTests>();
            var boundles = ReflactionHelper.GetResourcesBoundles().ToList();

            var type = typeof(ResourceGenericQueryTests<>);

            foreach (var boundle in boundles)
            {
                var genericType = ReflactionHelper.FillGenericType(type, boundle);
                var obj = UnityContainer.Resolve(genericType);
                tests.Add(obj as IResourceGenericQueryTests);
            }
        }

        [TestMethod]
        public virtual void TestList()
        {
            foreach (var test in tests)
                test.TestList();
        }

        [TestMethod]
        public virtual void TestCreateAddedToList()
        {
            foreach (var test in tests)
                test.TestCreateAddedToList();
        }

        [TestMethod]
        public virtual void TestGet()
        {
            foreach (var test in tests)
                test.TestGet();
        }

        [TestMethod]
        public virtual void TestGetNotFound()
        {
            foreach (var test in tests)
                test.TestGetNotFound();
        }
    }
}
