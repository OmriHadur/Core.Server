using Core.Server.Shared.Resources.User;

namespace Core.Server.Common.Helpers
{
    public interface ICurrentUserGetter
    {
        UserResource CurrentUser { get; set; }
    }
}
