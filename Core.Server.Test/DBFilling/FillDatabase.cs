using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Server.Client.Interfaces;
using Core.Server.Shared.Resources;
using Core.Server.Shared.Resources.Users;
using Core.Server.Tests.ResourceTests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Server.Tests.DBFilling
{
    [TestClass]
    public class FillDatabase : TestsBase
    {
        protected int SCALE = 5;
        protected readonly Random random = new Random();

        [TestMethod]
        public void FillUsers()
        {
            for (int i = 0; i < SCALE - 1; i++)
                ResourcesHolder.Create<UserResource>();
            ResourcesHolder.DeleteAll<LoginResource>();
        }

        protected List<TResource> GetAll<TCreateResource, TUpdateResource, TResource>()
            where TCreateResource : CreateResource
            where TUpdateResource : UpdateResource
            where TResource : Resource
        {
            var response = GetClient<IRestClient<TCreateResource, TUpdateResource,TResource>>().Get().Result;
            return response.Value.ToList();
        }

        protected T GetRadomResources<T>(List<T> resources)
        {
            return resources[random.Next(resources.Count)];
        }
    }
}