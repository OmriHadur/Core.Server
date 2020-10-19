using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Server.Shared.Resources.Users;

namespace Core.Server.Tests.ResourceTests
{
    public class LoginQueryTests
        : ResourceGenericQueryTests<LoginResource>
    {
        [TestMethod]
        public override void TestList()
        {
            Assert.AreEqual(2, GetAllExistingCount());
        }

        //    public override void TestUpdate() { }
        //    [TestMethod]
        //    public void TestLoginWithBadEmail()
        //    {
        //        var response = ResourcesHolder.EditAndCreate<LoginCreateResource, LoginUpdateResource, LoginResource >((r) => r.Email = "a" + r.Email);

        //        Assert.IsTrue(response.Result is UnauthorizedResult);
        //    }

        //    [TestMethod]
        //    public void TestLoginWithBadPassword()
        //    {
        //        var response = ResourcesHolder.EditAndCreate<LoginCreateResource, LoginUpdateResource, LoginResource >((r) => r.Password = "a" + r.Password);

        //        Assert.IsTrue(response.Result is UnauthorizedResult);
        //    }

        //    [TestMethod]
        //    public void TestLoginAfterUserDelete()
        //    {
        //        var loginCreateResource = ResourceCreator.GetRandomCreateResource();
        //        ResourcesHolder.DeleteAll<UserResource>();

        //        var response = ResourcesHolder.Create<LoginCreateResource, LoginUpdateResource, LoginResource >(loginCreateResource);

        //        Assert.IsTrue(response.Result is UnauthorizedResult);
        //    }
        //}
    }
}
