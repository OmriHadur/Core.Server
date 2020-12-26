namespace Core.Server.Test.ResourceTests.Interfaces
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