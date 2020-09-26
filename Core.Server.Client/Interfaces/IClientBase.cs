namespace Core.Server.Client.Interfaces
{
    public interface IClientBase
    {
        string ServerUrl { get; set; }

        string Token { get; set; }
    }
}