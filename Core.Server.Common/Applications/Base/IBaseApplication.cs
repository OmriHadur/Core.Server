using Core.Server.Shared.Resources.Users;

namespace Core.Server.Common.Applications
{
    public interface IBaseApplication
    {
        public UserResource CurrentUser { get; set; }
    }
}