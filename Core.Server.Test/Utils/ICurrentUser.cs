using System;
namespace Core.Server.Tests.Utils
{
    public interface ICurrentUser
    {
        event EventHandler<string> OnTokenChange;
        string Token { get; }
        string Email { get; }
        void Login();
        void LoginAs(string email);
        void Relogin();
        void Logout();
    }
}