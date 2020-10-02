using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Core.Server.Common;

namespace Core.Server.Persistence.Repositories
{
    [Inject]
    public class UserRepository : 
        Repository<UserEntity>, 
        IUserRepository
    {
    }
}