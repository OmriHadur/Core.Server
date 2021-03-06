﻿using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using Core.Server.Test.ResourceCreators.Interfaces;
using Unity;

namespace Core.Server.Test.ResourceCreation
{
    [Inject]
    public class UserRoleRandomResourceCreator
        : RandomResourceCreator<UserRoleAlterResource, UserRoleResource>
    {
        [Dependency]
        public IResourceCreate<RoleResource> RoleResourceCreate;

        protected override void AddRandomValues(UserRoleAlterResource createResource)
        {
            createResource.Id = RoleResourceCreate.GetOrCreate().Id;
        }

        protected override void AddRandomToExistingValues(UserRoleAlterResource alterResource, UserRoleResource existingResource)
        {
            alterResource.Id = RoleResourceCreate.Create().Value.Id;
        }
    }
}