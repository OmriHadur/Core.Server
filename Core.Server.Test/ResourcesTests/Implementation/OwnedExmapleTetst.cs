using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Core.Server.Tests.ResourceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Unity;

namespace Core.Server.Test.ResourcesTests.Implementation
{
    [TestClass]
    public class OwnedExmapleTetst
         : GenericExtraTestBase<ExampleChildCreateResource, ExampleUpdateResource, ExampleResource>
    {
        [Dependency]
        public IResourceOwned<ExampleResource> ResourceOwned;

        protected override object GetThis() => this;

        [TestMethod]
        public void TestOwnedGetAll()
        {
            CreateResource();
            CurrentUser.AddRole(typeof(ExampleResource), ResourceActions.All);
            CurrentUser.ReLogin();
            CreateResource();
            var response = ResourceOwned.GetAll();
            Assert.AreEqual(1, response.Value.Count());
            Validate(CreatedResource, response.Value.FirstOrDefault());
        }

        [TestMethod]
        public void TestOwnedAny()
        {
            CreateResource();
            CurrentUser.ReLogin();
            var response = ResourceOwned.Any();
            AssertNotFound(response);
        }

        [TestMethod]
        public void TestOwnedReassign()
        {
            CreateResource();
            var lastResource = CreatedResource.Id;
            CurrentUser.AddRole(typeof(ExampleResource), ResourceActions.All);
            CurrentUser.ReLogin();
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
            CurrentUser.ReLogin();

            var reassigenResponse = ResourceOwned.Reassigen(CreatedResource.Id);
            AssertUnauthorized(reassigenResponse);
        }

        [TestMethod]
        public void TestOwnedAuthorizedReassign()
        {
            CreateResource();
            CurrentUser.AddRole(typeof(ExampleResource), ResourceActions.All);
            CurrentUser.ReLogin();

            var reassigenResponse = ResourceOwned.Reassigen(CreatedResource.Id);
            AssertOk(reassigenResponse);
        }
    }
}
