using Core.Server.Application.Mappers.Base;
using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Mappers;
using Core.Server.Common.Repositories;
using Core.Server.Shared.Resources;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application.Mappers.Implementation
{
    [Inject]
    public class LoginResourceMapper
        : ResourceMapper<LoginResource, LoginEntity>
    {
        [Dependency]
        public ILookupRepository<UserEntity> UserLookupRepository { get; set; }

        [Dependency]
        public IResourceMapper<UserResource, UserEntity> UserResourceMapper { get; set; }

        public override async Task<LoginResource> Map(LoginEntity entity)
        {
            var loginResource = Mapper.Map<LoginResource>(entity);
            var userEntity = await UserLookupRepository.Get(entity.UserId);
            loginResource.User = await UserResourceMapper.Map(userEntity); ;
            return loginResource;
        }
    }
}
