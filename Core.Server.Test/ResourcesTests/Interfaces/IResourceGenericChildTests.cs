namespace Core.Server.Tests.ResourceTests.Interfaces
{
    public interface IResourceGenericChildTests
        : IResourceGenericTests
    {
        void TestCreate();
        void TestReplace();
        void TestUpdate();
        void TestDelete();

    }
}