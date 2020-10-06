using Core.Server.Application.Helper;
using Core.Server.Application.Mappers.Base;
using Core.Server.Common;
using Core.Server.Common.Entities;
using Core.Server.Common.Entities.Helpers;
using Core.Server.Common.Mappers;
using Core.Server.Common.Repositories;
using Core.Server.Shared.Resources.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application.Mappers.Implementation
{
    public class UserAlterResourceMapper
        : AlterResourceMapper<UserCreateResource,UserUpdateResource, UserResource,UserEntity>
    {
        private PasswordHasher _passwordHasher = new PasswordHasher();

        public async override Task<UserEntity> Map(UserCreateResource resource)
        {
            var userEntity = await base.Map(resource);
            byte[] passwordHash, passwordSalt;
            _passwordHasher.CreatePasswordHash(resource.Password, out passwordHash, out passwordSalt);
            userEntity.PasswordHash = passwordHash;
            userEntity.PasswordSalt = passwordSalt;
            return userEntity;
        }
    }
}
