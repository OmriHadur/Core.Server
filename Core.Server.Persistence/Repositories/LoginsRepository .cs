using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Core.Server.Common;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Core.Server.Persistence.Repositories
{
    [Inject]
    public class LoginsRepository : MongoRepository<LoginEntity>, ILoginsRepository 
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