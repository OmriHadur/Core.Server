using Core.Server.Client.Results;
using Core.Server.Shared.Query;
using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Core.Server.Tests.ResourceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace Core.Server.Test.ResourcesTests.Implementation
{
    [TestClass]
    public class OwnedExmapleTetst
         : GenericExtraTestBase<ExampleCreateResource, ExampleUpdateResource, ExampleResource>
    {
        [Dependency]
        public IResourceOwned<ExampleResource> ResourceOwned;

        protected override object GetThis() => this;

        [TestMethod]
        public void TestQueryEquals()
        {
            //ResourceOwned.
        }
    }
}
