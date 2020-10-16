using Core.Server.Shared.Resources.Users;
using Core.Server.Tests.Configuration;
using Core.Server.Tests.ResourceCreators.Interfaces;
using System;
using Unity;

namespace Core.Server.Tests.Utils
{
    public class CurrentUser : ICurrentUser
    {
        private string token;
        private bool logginin;

        [Dependency]
        public IResourcesIdsHolder ResourcesHolder;

        [Dependency]
        public TestConfig Config;

        [Dependency]
        public IResourceCreate<UserResource> UserResourceCreate;

        [Dependency]
        public IResourceCreate<LoginResource> LoginResourceCreate;

        public event EventHandler<string> OnTokenChange;

        public string Token
        {
            get
            {
                if (token == null && !logginin)
                    LoginWithNewUser();
                return token;
            }
        }

        public void Login()
        {
            if (!string.IsNullOrEmpty(token))
                return;

            logginin = true;
            var login = LoginResourceCreate.GetOrCreate();
            token = login.Token;
            logginin = false;
            OnTokenChange?.Invoke(this, token);
        }

        public void LoginWithNewUser()
        {
            Logout();
            LoginResourceCreate.Create();
            Login();
        }


        public void Logout()
        {
            token = string.Empty;
            OnTokenChange?.Invoke(this, token);
        }
    }
}
