using RestApi.Common.Entities;
using RestApi.Common.Repositories;
using RestApi.Common;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace RestApi.Persistence.Repositories
{
    [Inject]
    public class LoginRepository : MongoRepository<LoginEntity>, ILoginRepository
    {
        public async Task DeleteByUserId(string userId)
        {
            await Entities.DeleteManyAsync(e => e.UserId == userId);
        }

        public async Task<LoginEntity> GetByUserId(string userId)
        {
            return await FindFirst(le => le.UserId == userId);
        }
    }
}