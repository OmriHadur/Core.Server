using System;
using System.Collections.Generic;
using System.Text;

namespace RestApi.Tests.Unity
{
    public interface ITestsUnityContainer
    {
        T Resolve<T>();

        T Resolve<T>(string name);
    }
}
