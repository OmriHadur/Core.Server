using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System;
using Core.Server.Client.Results;

namespace Core.Server.Tests.ResourceTests
{
    public abstract class TestsBase
    {
        protected void Validate(object expected, object actual)
        {
            Assert.IsNotNull(actual);
            var properties = expected.GetType().GetProperties();
            foreach (var property in properties)
                ValidateProperty(expected, actual, property);
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

        protected void AssertBadRequestReason<TReson>(ActionResult response, TReson badRequestReason)
            where TReson : struct, Enum
        {
            var result = response as BadRequestResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(Convert.ToInt32(badRequestReason), result.Reason);
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
