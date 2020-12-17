using Core.Server.Common.Config;
using Core.Server.Common.Entities.Helpers;
using Core.Server.Shared.Resources;
using Core.Server.Shared.Resources.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Web.Authorization
{
    public class DocumentAuthorizationHandler : IAuthorizationService
    {
        [Dependency]
        public IJwtManager JwtManager;

        [Dependency]
        public Config Config;

        public async Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal claims, object resourceType, IEnumerable<IAuthorizationRequirement> requirements)
        {
            var userResource = JwtManager.GetUser(claims);
            if (userResource?.Email == Config.AppSettings.AdminUserName) 
                return AuthorizationResult.Success();

            var allowedActions = GetAllowedActions(userResource, resourceType.ToString()).ToList();

            if (IsAllowed(allowedActions, requirements.First()))
                return AuthorizationResult.Success();
            else
                return AuthorizationResult.Failed();
        }

        private bool IsAllowed(IEnumerable<ResourceActions> allowedActions, IAuthorizationRequirement requirement)
        {
            var neededAction = GetResourceActions(requirement as OperationAuthorizationRequirement);
            return allowedActions.Any(action => action.HasFlag(neededAction));
        }

        private ResourceActions GetResourceActions(OperationAuthorizationRequirement operationAuthorization)
        {
            if (operationAuthorization.Name == Operations.Read.Name)
                return ResourceActions.Read;
            if (operationAuthorization.Name == Operations.Create.Name)
                return ResourceActions.Create;
            if (operationAuthorization.Name == Operations.Alter.Name)
                return ResourceActions.Alter;
            if (operationAuthorization.Name == Operations.Delete.Name)
                return ResourceActions.Delete;
            return ResourceActions.None;
        }

        private IEnumerable<ResourceActions> GetAllowedActions(UserResource userResource, string resource)
        {
            var allowedActions = GetResourceActions(Config.AllowAnonymous, resource);
            if (userResource != null)
                foreach (var role in userResource.Roles)
                    allowedActions.Union(GetResourceActions(role.Policies, resource));
            return allowedActions;
        }

        private IEnumerable<ResourceActions> GetResourceActions(PolicyResource[] policies,string resource)
        {
            foreach (var policy in policies)
                if (policy.ResourceType == resource)
                    yield return policy.ResourceActions;
        }

        public Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object resource, string policyName)
        {
            throw new NotImplementedException();
        }
    }
}