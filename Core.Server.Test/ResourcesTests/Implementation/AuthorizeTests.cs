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
        [TestMethod]
        public void TestUnauthorized()
        {
            CurrentUser.Login();
            AssertUnauthorized(ResourceLookup.Get());
            AssertUnauthorized(ResourceCreate.Create());
            AssertUnauthorized(ResourceAlter.Update());
        }

        [TestMethod]
        public void TestAuthorizedRead()
        {
            CurrentUser.AddRole(typeof(ExampleResource), ResourceActions.Read);
            AssertOk(ResourceLookup.Get());
        }

        [TestMethod]
        public void TestAuthorizedCreate()
        {
            CurrentUser.AddRole(typeof(ExampleResource), ResourceActions.Create);
            AssertOk(ResourceCreate.Create());
        }

        [TestMethod]
        public void TestAuthorizedAlter()
        {
            CurrentUser.AddRole(typeof(ExampleResource), ResourceActions.Alter);
            AssertOk(ResourceAlter.Update());
        }
    }
}
