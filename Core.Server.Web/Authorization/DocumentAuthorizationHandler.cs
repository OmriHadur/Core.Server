using Core.Server.Common.Entities.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
            var roles = JwtManager.GetUser(claims).Roles;

            foreach (var requirement in requirements)
            {
                var operationRequirement = (requirement as OperationAuthorizationRequirement).Name;
                return AuthorizationResult.Failed();
            }

            return AuthorizationResult.Success();
        }

        public Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object resource, string policyName)
        {
            throw new NotImplementedException();
        }
    }
}
