using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Server.Shared.Errors;
using Core.Server.Shared.Resources.Users;

namespace Core.Server.Tests.ResourceTests
{
    [TestClass]
    public class UserResourceTests : ResourceTests<UserCreateResource,UserUpdateResource, UserResource>
    {
        [TestMethod]
        public override void TestList()
        {
            Assert.AreEqual(2, GetAllExistingCount());
        }

        [TestMethod]
        public void TestReCreate()
        {
            var userResource = ResourcesHolder.GetLastOrCreate<UserResource>().Value;
            var response = ResourcesHolder.EditAndCreate<UserCreateResource, UserUpdateResource, UserResource >(u => u.Email = userResource.Email);
            AssertBadRequestReason(response, BadRequestReason.SameExists);
        }
    }
}
