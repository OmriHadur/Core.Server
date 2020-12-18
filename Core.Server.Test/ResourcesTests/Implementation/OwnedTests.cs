using Core.Server.Shared.Resources;
using Core.Server.Test.ResourceCreators.Interfaces;
using Core.Server.Test.ResourceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Unity;

namespace Core.Server.Test.ResourcesTests.Implementation
{
    [TestClass]
    public class OwnedTests
         : GenericExtraTestBase<ExampleChildCreateResource, ExampleUpdateResource, ExampleResource>
    {
        [Dependency]
        public IResourceOwned<ExampleResource> ResourceOwned;

        [TestMethod]
        public void TestOwnedGetAll()
        {
            CreateResource();
            CurrentUser.AddRoleAndRelogin(typeof(ExampleResource), ResourceActions.All);
            CreateResource();
            var response = ResourceOwned.GetAll();
            Assert.AreEqual(1, response.Value.Count());
            Validate(CreatedResource, response.Value.FirstOrDefault());
        }

        [TestMethod]
        public void TestOwnedAny()
        {
            CreateResource();
            CurrentUser.Login();
            var response = ResourceOwned.Any();
            AssertNotFound(response);
        }

        [TestMethod]
        public void TestOwnedReassign()
        {
            CreateResource();
            var lastResource = CreatedResource.Id;
            CurrentUser.AddRoleAndRelogin(typeof(ExampleResource), ResourceActions.All);
            CreateResource();

            var reassigenResponse = ResourceOwned.Reassigen(lastResource);
            AssertOk(reassigenResponse);
            var getAllResponse = ResourceOwned.GetAll();

            Assert.AreEqual(2, getAllResponse.Value.Count());
        }

        [TestMethod]
        public void TestOwnedUnauthorizedReassign()
        {
            CreateResource();
            CurrentUser.Login();

            var reassigenResponse = ResourceOwned.Reassigen(CreatedResource.Id);
            AssertUnauthorized(reassigenResponse);
        }

        [TestMethod]
        public void TestOwnedAuthorizedReassign()
        {
            CreateResource();
            CurrentUser.AddRoleAndRelogin(typeof(ExampleResource), ResourceActions.All);

            var reassigenResponse = ResourceOwned.Reassigen(CreatedResource.Id);
            AssertOk(reassigenResponse);
        }
    }
}
