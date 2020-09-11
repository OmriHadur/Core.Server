
using RestApi.Common.Entities;
using RestApi.Common.Repositories;
using System.Threading.Tasks;
using RestApi.Common;

namespace RestApi.Persistence.Repositories
{
    [Inject]
    public class UserRepository : MongoRepository<UserEntity>, IUserRepository
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