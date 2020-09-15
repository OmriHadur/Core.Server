﻿using RestApi.Shared.Resources.Users;
using RestApi.Tests.ResourceCreators.Interfaces;
using System;
using Unity;

namespace RestApi.Tests.Utils
{
    public class TokenHandler : ITokenHandler
    {
        private string token;
        private bool logginin;

        [Dependency]
        public IResourcesHolder ResourcesHolder;

        [Dependency]
        public IConfigHandler ConfigHandler;

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
            if (string.IsNullOrEmpty(token))
                LoginWithNewUser();
        }

        public void LoginWithNewUser()
        {
            logginin = true;
            var user = ResourcesHolder.Create<UserResource>().Value;
            var loginCreateResource = new LoginCreateResource() { Email = user.Email, Password = ConfigHandler.Config.UserPassword };
            var login = ResourcesHolder.Create<LoginCreateResource, LoginResource>(loginCreateResource).Value;
            token = login.Token;
            logginin = false;
            OnTokenChange?.Invoke(this, token);
        }


        public void Logout()
        {
            token = string.Empty;
            OnTokenChange?.Invoke(this, token);
        }
    }
}