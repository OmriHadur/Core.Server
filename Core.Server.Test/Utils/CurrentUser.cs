using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources.Users;
using Core.Server.Tests.Configuration;
using Core.Server.Tests.ResourceCreators.Interfaces;
using System;
using Unity;

namespace Core.Server.Tests.Utils
{
    [Inject]
    public class CurrentUser : ICurrentUser
    {
        private string token;
        private bool logginin;

        [Dependency]
        public IResourcesIdsHolder ResourcesHolder;

        [Dependency]
        public TestConfig Config;

        [Dependency]
        public Lazy<IResourceCreate<UserResource>> UserResourceCreate;

        [Dependency]
        public Lazy<IResourceCreate<LoginResource>> LoginResourceCreate;

        public event EventHandler<string> OnTokenChange;
        public string Email { get; private set; }

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
            if (!string.IsNullOrEmpty(token) | logginin)
                return;

            logginin = true;
            Email = UserResourceCreate.Value.GetOrCreate().Email;
            var login = LoginResourceCreate.Value.GetOrCreate();
            token = login.Token;
            logginin = false;
            OnTokenChange?.Invoke(this, token);
        }

        public void LoginWithNewUser()
        {
            Logout();
            Login();
        }


        public void Logout()
        {
            token = string.Empty;
            OnTokenChange?.Invoke(this, token);
        }
    }
}
