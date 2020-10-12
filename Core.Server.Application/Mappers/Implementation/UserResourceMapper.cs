﻿using Core.Server.Application.Helper;
using Core.Server.Application.Mappers.Base;
using Core.Server.Common.Entities;
using Core.Server.Shared.Resources.Users;
using System.Threading.Tasks;
using Unity;
using Core.Server.Common.Attributes;

namespace Core.Server.Application.Mappers.Implementation
{
    [Inject]
    public class UserResourceMapper
        : AlterResourceMapper<UserCreateResource,UserUpdateResource, UserResource,UserEntity>
    {
        private PasswordHasher _passwordHasher = new PasswordHasher();

        public async override Task<UserEntity> Map(UserCreateResource resource)
        {
            var userEntity = await base.Map(resource);
            AddPassword(resource.Password, userEntity);
            return userEntity;
        }

        public async override Task Map(UserCreateResource resource, UserEntity entity)
        {
            AddPassword(resource.Password, entity);
        }

        public async override Task Map(UserUpdateResource resource, UserEntity entity)
        {
            await base.Map(resource, entity);
            AddPassword(resource.Password, entity);
        }

        private void AddPassword(string password, UserEntity userEntity)
        {
            byte[] passwordHash, passwordSalt;
            _passwordHasher.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            userEntity.PasswordHash = passwordHash;
            userEntity.PasswordSalt = passwordSalt;
        }
    }
}