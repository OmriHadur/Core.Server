﻿
namespace Core.Server.Shared.Resources.Users
{
    public class LoginResource : Resource
    {
        public string Token { get; set; }

        public UserResource User { get; set; }
    }
}