using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using Core.Server.Test.ResourceCreators.Interfaces;
using Core.Server.Test.ResourcesCreators.Interfaces;
using Core.Server.Test.Unity;
using Core.Server.Test.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace Core.Server.Test.ResourceTests
{
    public abstract class TestsBase
    {
        [Dependency]
        public IResourcesClean ResourcesClean;

        [Dependency]
        public IResourcesIdsHolder ResourcesIdsHolder;

        [Dependency]
        public ICurrentUser CurrentUser;

        [TestInitialize]
        public virtual void TestInit()
        {
            var testsUnityContainer = new TestsUnityContainer();
            testsUnityContainer.UnityContainer.BuildUp(This.GetType(), This);
        }

        protected object This => this;

        public virtual void Cleanup()
        {
            CurrentUser.LoginAsAdmin();
            ResourcesClean.Clean();
        }

        protected void ValidateList<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            for (int i = 0; i < expected.Count(); i++)
                Validate(expected.ElementAt(i), actual.ElementAt(i));
        }

        protected void ValidateNotEqual<T>(T expected, T actual)
        {
            try
            {
                Validate(expected, actual);
            }
            catch (AssertFailedException e)
            {
                return;
            }
            Assert.Fail();
        }
        protected void Validate<T>(T expected, T actual)
        {
            Assert.IsNotNull(actual);
            if (expected.GetType().IsArray)
            {
                var expectedArray = expected as T[];
                var actualArray = actual as T[];
                for (int i = 0; i < expectedArray.Length; i++)
                {
                    Validate(expectedArray[i], actualArray[i]);
                }
            }
            else
            {
                var properties = expected.GetType().GetProperties();
                foreach (var property in properties)
                    ValidateProperty(expected, actual, property);
            }
        }

        protected void AssertUnauthorized(ActionResult response)
        {
            Assert.IsTrue(response is UnauthorizedResult);
        }
        protected void AssertUnauthorized<T>(ActionResult<T> response)
        {
            Assert.IsInstanceOfType(response.Result, typeof(UnauthorizedResult));
        }

        protected void AssertNotFound(ActionResult response)
        {
            Assert.IsInstanceOfType(response, typeof(NotFoundResult));
        }

        protected void AssertNotFound<T>(ActionResult<T> response)
        {
            Assert.IsInstanceOfType(response.Result, typeof(NotFoundResult));
        }

        protected void AssertNotErrors<T>(ActionResult<T> response)
        {
            Assert.IsNull(response.Result);
        }

        protected void AssertNoError<T>(ActionResult<T> response)
        {
            Assert.IsTrue(response.Value != null);
        }

        protected void AssertOk<T>(ActionResult<T> response)
        {
            Assert.IsTrue(response.IsSuccess);
        }

        protected void AssertOk(ActionResult response)
        {
            Assert.IsTrue(response is OkResult);
        }

        protected void AssertBadRequestReason<T, TReson>(ActionResult<T> response, TReson badRequestReason)
            where TReson : struct, Enum
        {
            AssertBadRequestReason(response.Result, badRequestReason);
        }

        protected void AssertValidationError<T>(ActionResult<T> response)
        {
            Assert.IsTrue(response.Result is ValidationErrorResult);
        }

        protected void AssertBadRequestReason<TReson>(ActionResult response, TReson badRequestReason)
            where TReson : struct, Enum
        {
            var result = response as BadRequestResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(Convert.ToInt32(badRequestReason), result.Reason);
        }

        protected void AssertAreEqual<TResource>(TResource expected, ActionResult<IEnumerable<TResource>> actual)
            where TResource : Resource
        {
            Assert.IsTrue(actual.IsSuccess);
            Assert.AreEqual(1, actual.Value.Count());
            Validate(expected, actual.Value.First());
        }

        protected string GetRandomId()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 24);
        }

        private void ValidateProperty(object expected, object actual, System.Reflection.PropertyInfo property)
        {
            var expectedValue = property.GetValue(expected);
            var actualProperty = actual.GetType().GetProperties().FirstOrDefault(p => p.Name == property.Name);
            var propertyType = actualProperty?.PropertyType;
            if (actualProperty != null && !propertyType.IsGenericType && propertyType != typeof(DateTime))
            {
                var actualValue = actualProperty.GetValue(actual);
                ValidateValue(expectedValue, actualProperty, propertyType, actualValue);
            }
        }

        private void ValidateValue(object expectedValue, System.Reflection.PropertyInfo actualProperty, Type propertyType, object actualValue)
        {
            if (actualValue == null) return;
            if (propertyType.IsPrimitive || propertyType == typeof(string))
                Assert.AreEqual(expectedValue, actualValue, "With Property " + actualProperty.Name);
            else if (actualValue.GetType() == typeof(string[]))
                Assert.IsTrue(((string[])expectedValue).SequenceEqual((string[])actualValue));
            else
                Validate(expectedValue, actualValue);
        }
    }
}
