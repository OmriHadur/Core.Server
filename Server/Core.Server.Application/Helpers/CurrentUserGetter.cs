using Core.Server.Common.Attributes;
using Core.Server.Common.Helpers;
using Core.Server.Shared.Resources.User;

namespace Core.Server.Application.Helpers
{
    [Inject]
    public class CurrentUserGetter : ICurrentUserGetter
    {
        public UserResource CurrentUser { get; set; }
    }
}
