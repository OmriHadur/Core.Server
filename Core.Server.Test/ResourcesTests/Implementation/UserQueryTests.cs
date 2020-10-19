using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Server.Shared.Resources.Users;

namespace Core.Server.Tests.ResourceTests
{
    public class UserQueryTests
        : ResourceGenericQueryTests<UserResource>
    {
        [TestMethod]
        public override void TestList()
        {
            Assert.AreEqual(2, GetAllExistingCount());
        }

        //[TestMethod]
        //public void TestReCreate()
        //{
        //    var userResource = create
        //    var response = ResourcesHolder.EditAndCreate<UserCreateResource, UserUpdateResource, UserResource>(u => u.Email = userResource.Email);
        //    AssertBadRequestReason(response, BadRequestReason.SameExists);
        //}
    }
}
