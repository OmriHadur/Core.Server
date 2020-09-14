using AutoMapper;
using RestApi.Common.Entities;
using RestApi.Shared.Resources;
using RestApi.Shared.Resources.Users;

namespace RestApi.Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            AddMapping<UserCreateResource, UserResource, UserEntity>();
            AddMapping<LoginCreateResource, LoginResource, LoginEntity>();
        }

        public void AddMapping<TCreateResource, TResource, TEntity>()
            where TCreateResource : CreateResource
            where TResource : Resource
            where TEntity : Entity
        {
            CreateMap<TCreateResource, TEntity>();
            CreateMap<TResource, TEntity>().ReverseMap();
        }
    }
}