using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using Core.Server.Test.ResourceCreators.Interfaces;
using Core.Server.Test.ResourceTests.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Unity;

namespace Core.Server.Test.ResourceTests
{
    [Inject]
    public class GenericChildTests<TChildAlterResource, TParentResource, TChildResource>
        : TestsBase
        , IResourceGenericChildTests
        where TChildAlterResource : ChildAlterResource
        where TParentResource : Resource
        where TChildResource : Resource
    {

        private TParentResource parentCreated;

        private TChildResource childCreated;

        [Dependency]
        public IChildResourceAlter<TChildAlterResource, TParentResource, TChildResource> ChildResourceAlter;

        [Dependency]
        public IResourceLookup<TParentResource> ParentResourceLookup;

        public override void TestInit()
        {
            parentCreated = ChildResourceAlter.Create().Value;
            childCreated = ChildResourceAlter.GetChildResource(parentCreated).Last();
        }

        public void TestCreate()
        {
            Assert.IsNotNull(childCreated);
            Validate(childCreated, GetChild());
        }

        public void TestDelete()
        {
            ChildResourceAlter.DeleteLastChild();
            Assert.IsNull(GetChild());
        }

        public void TestReplace()
        {
            ChildResourceAlter.Replace();
            var child = GetChild();
            ValidateNotEqual(childCreated, child);
        }

        public void TestUpdate()
        {
            ChildResourceAlter.Update();
            var child = GetChild();
            ValidateNotEqual(childCreated, child);
        }

        private TChildResource GetChild()
        {
            var parent = ParentResourceLookup.Get(parentCreated.Id).Value;
            return ChildResourceAlter.GetChildResource(parent).LastOrDefault();
        }
    }
}