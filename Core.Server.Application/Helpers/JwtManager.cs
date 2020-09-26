using Core.Server.Common.Entities.Helpers;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Core.Server.Common;
using Core.Server.Shared.Resources.Users;

namespace Core.Server.Application.Helper
{
    [Inject]
    public class JwtManager : IJwtManager
    {
        private const string UserResourceKey = "UserResource";

        public string GenerateToken(UserResource user, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {new Claim(UserResourceKey,JsonConvert.SerializeObject(user))}),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public UserResource GetUser(ClaimsPrincipal ClaimsPrincipal)
        {
            var claim = ClaimsPrincipal.Claims.FirstOrDefault();
            if (claim == null) return null;
            return JsonConvert.DeserializeObject<UserResource>(claim.Value);
        }
    }
}
