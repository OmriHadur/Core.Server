﻿using Core.Server.Shared.Resources;
using Core.Server.Test.ResourceCreators.Interfaces;
using Core.Server.Test.ResourceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Unity;

namespace Core.Server.Test.ResourcesTests.Implementation
{
    [TestClass]
    public class BatchChildTests
         : GenericExtraTestBase<ExampleChildCreateResource, ExampleChildUpdateResource, ExampleResource>
    {
        [Dependency]
        public IChildResourceBatch<ExampleChildCreateResource, ExampleChildUpdateResource, ExampleResource, ExampleChildResource> ResourceBatch;

        protected override object GetThis() => this;

        [TestMethod]
        public void TestBatchCreate()
        {
            var result = ResourceBatch.Create(5);
            AssertOk(result);
            var parents = ResourceLookup.Get().Value;
            var childrenAmount = parents.Sum(p => p.ChildResources.Length);
            Assert.AreEqual(5, childrenAmount);
        }
    }
}