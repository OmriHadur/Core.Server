using Core.Server.Shared.Resources;
using Core.Server.Test.ResourceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Server.Test.ResourcesTests.Implementation
{
    [TestClass]
    public class AuthorizeTests
        : GenericExtraTestBase<ExampleCreateResource, ExampleUpdateResource, ExampleResource>
    {
        [TestMethod]
        public void TestUnauthorizedRead()
        {
            CurrentUser.Login();
            AssertUnauthorized(ResourceLookup.Get());
        }

        [TestMethod]
        public void TestAuthorizedRead()
        {
            CurrentUser.AddRoleAndRelogin(typeof(ExampleResource), ResourceActions.Read);
            AssertOk(ResourceLookup.Get());
        }

        [TestMethod]
        public void TestAuthorizedCreate()
        {
            CurrentUser.AddRoleAndRelogin(typeof(ExampleResource), ResourceActions.Create);
            AssertOk(ResourceCreate.Create());
        }

        [TestMethod]
        public void TestUnauthorizedCreate()
        {
            CurrentUser.Login();
            AssertUnauthorized(ResourceCreate.Create());
        }

        [TestMethod]
        public void TestUnauthorizedAlter()
        {
            ResourceCreate.Create();
            CurrentUser.AddRoleAndRelogin(typeof(ExampleResource), ResourceActions.Read);
            AssertUnauthorized(ResourceAlter.Update());
        }

        [TestMethod]
        public void TestAuthorizedAlter()
        {
            ResourceCreate.Create();
            CurrentUser.AddRoleAndRelogin(typeof(ExampleResource), ResourceActions.Read | ResourceActions.Alter);
            AssertOk(ResourceAlter.Update());
        }

        [TestMethod]
        public void TestUnauthorizedDelete()
        {
            ResourceCreate.Create();
            CurrentUser.Login();
            AssertUnauthorized(ResourceCreate.Delete());
        }

        [TestMethod]
        public void TestAuthorizedDelete()
        {
            ResourceCreate.Create();
            CurrentUser.AddRoleAndRelogin(typeof(ExampleResource), ResourceActions.Delete);
            AssertOk(ResourceCreate.Delete());
        }
    }
}
