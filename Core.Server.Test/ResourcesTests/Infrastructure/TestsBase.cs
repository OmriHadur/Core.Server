using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System;
using Core.Server.Tests.Unity;
using Core.Server.Client.Results;

namespace Core.Server.Tests.ResourceTests
{
    public abstract class TestsBase
    {
        protected Random Random;
        protected ITestsUnityContainer TestsUnityContainer;

        public TestsBase()
        {
            Random = new Random();
            TestsUnityContainer = new TestsUnityContainer();
        }

        protected void Validate(object expected, object actual)
        {
            Assert.IsNotNull(actual);
            var properties = expected.GetType().GetProperties();
            foreach (var property in properties)
            {
                var expectedValue = property.GetValue(expected);
                var actualProperty = actual.GetType().GetProperties().FirstOrDefault(p => p.Name == property.Name);
                var propertyType = actualProperty?.PropertyType;
                if (actualProperty != null && !propertyType.IsGenericType && propertyType != typeof(DateTime))
                {
                    var actualValue = actualProperty.GetValue(actual);
                    if (propertyType.IsPrimitive || propertyType == typeof(string))
                        Assert.AreEqual(expectedValue, actualValue, "With Property " + actualProperty.Name);
                    else if (actualValue.GetType() == typeof(string[]))
                        Assert.IsTrue(((string[])expectedValue).SequenceEqual((string[])actualValue));
                    else
                        Validate(expectedValue, actualValue);
                }
            }
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

        protected string RandomId
        {
            get
            {
                var buffer = new byte[12];
                Random.NextBytes(buffer);
                return string.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            }
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
    }
}
