﻿using Core.Server.Shared.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Server.Test.ResourceTests.ResourceExtra
{
    [TestClass]
    public class UserResourceExtraTests
        : GenericExtraTestBase<UserAlterResource, UserResource>
    {
        [TestMethod]
        public void TestReCreate()
        {
            CreateResource();
            var email = CreatedResource.Email;
            var response = ResourceAlter.Create(r => r.Email = email);
            AssertValidationError(response);
        }
    }
}
