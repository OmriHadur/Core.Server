using Core.Server.Shared.Resources.Users;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;

namespace Core.Server.Tests.ResourceTests
{
    [TestClass]
    public class LoginExtraTests
        : GenericExtraTestBase<LoginCreateResource, LoginUpdateResource, LoginResource>
    {
        [TestMethod]
        public void TestLoginWithBadEmail()
        {
            var response = ResourceAlter.Create(r => r.Email = r.Email + 'a');
            AssertUnauthorized(response);
        }

        [TestMethod]
        public void TestLoginWithBadPassword()
        {
            var response = ResourceAlter.Create(r => r.Password = r.Password + 'a');

            AssertUnauthorized(response);
        }

        [TestMethod]
        public void TestLoginAfterUserDelete()
        {
            CreateResource();
            var email = CreatedResource.User.Email;
            var userResourceCreate = UnityContainer.Resolve<IResourceCreate<UserResource>>();
            userResourceCreate.DeleteAll();
            var createResource = new LoginCreateResource() { Email = email, Password = TestConfig.UserPassword };

            var response = ResourceAlter.Create(createResource);

            AssertUnauthorized(response);
        }
    }
}
