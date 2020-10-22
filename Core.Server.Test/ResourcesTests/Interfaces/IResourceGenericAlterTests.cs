namespace Core.Server.Tests.ResourceTests.Interfaces
{
    public interface IResourceGenericAlterTests
        : IResourceGenericTests
    {
        void TestReplace();
        void TestUpdate();
    }
}