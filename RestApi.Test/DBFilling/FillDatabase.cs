using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestApi.Client.Interfaces;
using RestApi.Shared.Resources;
using RestApi.Shared.Resources.Users;
using RestApi.Tests.ResourceTests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestApi.Tests.DBFilling
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

        protected List<TResource> GetAll<TCreateResource, TResource>()
            where TCreateResource : CreateResource
            where TResource : Resource
        {
            var response = GetClient<IRestClient<TCreateResource, TResource>>().Get().Result;
            return response.Value.ToList();
        }

        protected T GetRadomResources<T>(List<T> resources)
        {
            return resources[random.Next(resources.Count)];
        }
    }
}