using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestApi.Shared.Errors;
using RestApi.Shared.Resources.Users;

namespace RestApi.Tests.ResourceTests
{
    [TestClass]
    public class UserResourceTests : ResourceTests<UserCreateResource, UserResource>
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
            var response = ResourcesHolder.EditAndCreate<UserCreateResource, UserResource>(u => u.Email = userResource.Email);
            AssertBadRequestReason(response, BadRequestReason.SameExists);
        }
    }
}
