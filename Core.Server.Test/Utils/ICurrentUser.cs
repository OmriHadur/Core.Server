using System;
namespace Core.Server.Tests.Utils
{
    public interface ICurrentUser
    {
        event EventHandler<string> OnTokenChange;
        string Token { get; }
        string Email { get; }
        void Login();
        void Relogin();
        void Logout();
    }
}