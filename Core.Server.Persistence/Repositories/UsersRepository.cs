
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using System.Threading.Tasks;
using Core.Server.Common;

namespace Core.Server.Persistence.Repositories
{
    [Inject]
    public class UsersRepository : MongoRepository<UserEntity>, IUsersRepository
    {
        public async Task<bool> EmailExists(string email)
        {
            return await GetByEmail(email) != null;
        }

        public async Task<UserEntity> GetByEmail(string email)
        {
            return await FindFirst(le => le.Email == email);
        }
    }
}