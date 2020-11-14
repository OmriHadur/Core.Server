using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Server.Shared.Resources.Users;
using Core.Server.Shared.Errors;

namespace Core.Server.Tests.ResourceTests
{
    [TestClass]
    public class UserExtraTests
        : GenericExtraTestBase<UserCreateResource, UserUpdateResource, UserResource>
    {
        [TestMethod]
        public void TestReCreate()
        {
            var email =  CreatedResource.Email;
            var response = ResourceAlter.Create(r => r.Email = email);
            AssertBadRequestReason(response, BadRequestReason.SameExists);
        }
    }
}
