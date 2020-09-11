using AutoMapper;
using RestApi.Common.Entities;
using RestApi.Standard.Shared.Resources;
using RestApi.Standard.Shared.Resources.Users;
using System;
using System.Linq;

namespace RestApi.Application.Mapping
{
    public class AutoMapperProfile : MappingBase
    {
        public AutoMapperProfile()
        {
            AddMapping<UserCreateResource, UserResource, UserEntity>();
            AddMapping<LoginCreateResource, LoginResource, LoginEntity>();
        }
    }
}