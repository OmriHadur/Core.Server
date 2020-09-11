using RestApi.Common.Entities;
using System.Threading.Tasks;

namespace RestApi.Common.Repositories
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        Task<bool> EmailExists(string email);

        Task<UserEntity> GetByEmail(string email);
    }
}
