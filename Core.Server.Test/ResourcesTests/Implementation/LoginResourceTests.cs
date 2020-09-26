using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Server.Client.Results;
using Core.Server.Shared.Resources.Users;

namespace Core.Server.Tests.ResourceTests
{
    [TestClass]
    public class LoginResourceTests : ResourceTests<LoginCreateResource, LoginResource>
    {
        [TestMethod]
        public override void TestList()
        {
            Assert.AreEqual(2, GetAllExistingCount());
        }

        public override void TestUpdate() { }
        [TestMethod]
        public void TestLoginWithBadEmail()
        {
            var response = ResourcesHolder.EditAndCreate<LoginCreateResource, LoginResource>((r) => r.Email = "a" + r.Email);

            Assert.IsTrue(response.Result is UnauthorizedResult);
        }

        [TestMethod]
        public void TestLoginWithBadPassword()
        {
            var response = ResourcesHolder.EditAndCreate<LoginCreateResource, LoginResource>((r) => r.Password = "a" + r.Password);

            Assert.IsTrue(response.Result is UnauthorizedResult);
        }

        [TestMethod]
        public void TestLoginAfterUserDelete()
        {
            var loginCreateResource = ResourceCreator.GetRandomCreateResource();
            ResourcesHolder.DeleteAll<UserResource>();

            var response = ResourcesHolder.Create<LoginCreateResource, LoginResource>(loginCreateResource);

            Assert.IsTrue(response.Result is UnauthorizedResult);
        }
    }
}
