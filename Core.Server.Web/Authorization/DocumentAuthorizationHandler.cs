using Core.Server.Common.Entities.Helpers;
using Core.Server.Shared.Resources;
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

        public async Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal claims, object resource, IEnumerable<IAuthorizationRequirement> requirements)
        {
            var resourceActions = GetResourceActions(claims, resource).ToList();

            foreach (var requirement in requirements)
                if (!IsAllowed(resourceActions, requirement))
                    return AuthorizationResult.Failed();

            return AuthorizationResult.Success();
        }

        private static bool IsAllowed(List<ResourceActions> resourceActions, IAuthorizationRequirement requirement)
        {
            var operationRequirement = (requirement as OperationAuthorizationRequirement).Name;
            return resourceActions.Any(action => action.ToString() == operationRequirement);
        }

        private IEnumerable<ResourceActions> GetResourceActions(ClaimsPrincipal claims, object resource)
        {
            var user = JwtManager.GetUser(claims);
            if (user != null)
                foreach (var role in user.Roles)
                    foreach (var policy in role.Policies)
                        if (policy.ResourceType == resource.ToString())
                            yield return policy.ResourceActions;
        }

        public Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object resource, string policyName)
        {
            throw new NotImplementedException();
        }
    }
}