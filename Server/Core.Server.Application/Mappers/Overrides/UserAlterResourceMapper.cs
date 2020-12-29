using Core.Server.Application.Helpers;
using Core.Server.Application.Mappers.Base;
using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Shared.Resources.User;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application.Mappers.Implementation
{
    [Inject]
    public class UserAlterResourceMapper: AlterResourceMapper<UserAlterResource, UserEntity>
    {
        private readonly PasswordHasher _passwordHasher = new PasswordHasher();

        public async override Task<UserEntity> MapCreate(UserAlterResource resource)
        {
            var userEntity = await base.MapCreate(resource);
            AddPassword(resource.Password, userEntity);
            return userEntity;
        }

        public async override Task MapReplace(UserAlterResource resource, UserEntity entity)
        {
            await base.MapReplace(resource, entity);
            if (resource.Password != null)
                AddPassword(resource.Password, entity);
        }

        public async override Task MapUpdate(UserAlterResource resource, UserEntity entity)
        {
            await base.MapReplace(resource, entity);
            if (resource.Password != null)
                AddPassword(resource.Password, entity);
        }

        private void AddPassword(string password, UserEntity userEntity)
        {
            _passwordHasher.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            userEntity.PasswordHash = passwordHash;
            userEntity.PasswordSalt = passwordSalt;
        }
    }
}
