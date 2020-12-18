using Core.Server.Shared.Resources;
using Core.Server.Test.ResourceCreators.Interfaces;
using Core.Server.Test.ResourceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;

namespace Core.Server.Test.ResourcesTests.Implementation
{
    [TestClass]
    public class AuthorizeTests
        : GenericExtraTestBase<ExampleCreateResource, ExampleUpdateResource, ExampleResource>
    {
        [Dependency]
        public IResourceAlter<ExampleCreateResource, ExampleUpdateResource, ExampleResource> ResourceAlter;

        [TestMethod]
        public void TestAuthorizedRead()
        {
            CurrentUser.AddRole(typeof(ExampleResource), ResourceActions.Read);
            AssertOk(ResourceLookup.Get());
        }

        [TestMethod]
        public void TestUnauthorizedRead()
        {
            CurrentUser.Login();
            AssertUnauthorized(ResourceLookup.Get());
        }

        [TestMethod]
        public void TestUnauthorizedCreate()
        {
            CurrentUser.Login();
            AssertUnauthorized(ResourceCreate.Create());
        }

        [TestMethod]
        public void TestAuthorizedCreate()
        {
            CurrentUser.AddRole(typeof(ExampleResource), ResourceActions.Create);
            AssertOk(ResourceCreate.Create());
        }

        [TestMethod]
        public void TestUnauthorizedAlter()
        {
            CurrentUser.Login();
            AssertUnauthorized(ResourceAlter.Update());
        }

        [TestMethod]
        public void TestAuthorizedAlter()
        {
            CurrentUser.AddRole(typeof(ExampleResource), ResourceActions.Alter);
            AssertOk(ResourceAlter.Update());
        }
    }
}
