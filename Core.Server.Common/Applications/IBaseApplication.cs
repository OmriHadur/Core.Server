using Core.Server.Shared.Resources.Users;
using System;

namespace Core.Server.Common.Applications
{
    public interface IBaseApplication
    {
        public UserResource CurrentUser { get; }
    }
}