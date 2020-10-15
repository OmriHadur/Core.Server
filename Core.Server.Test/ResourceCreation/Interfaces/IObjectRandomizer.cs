namespace Core.Server.Tests.ResourceCreation.Interfaces
{
    public interface IObjectRandomizer
    {
        void AddRandomValues(object resource);
        string GetRandomId();
        string GetRandomString(int length);
    }
}