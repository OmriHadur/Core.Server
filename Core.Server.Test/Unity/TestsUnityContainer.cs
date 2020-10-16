using Core.Server.Client.Clients;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Core.Server.Tests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity;

namespace Core.Server.Tests.Unity
{
    public class TestsUnityContainer : ITestsUnityContainer
    {
        private IUnityContainer unityContainer;

        public TestsUnityContainer()
        {
            unityContainer = new UnityContainer();
        }

        public T Resolve<T>()
        {
            return unityContainer.Resolve<T>();
        }

        public T Resolve<T>(string name)
        {
            return unityContainer.Resolve<T>(name);
        }
    }
}
