using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace apiCoreP.Services
{
    /// <summary>
    /// auth service
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userService"></param>
        public AuthService(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// token response
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Tuple<string, string> IdentityResponse(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new Tuple<string, string>(encodedJwt, identity.Name);
        }

        /// <summary>
        /// user claims
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<ClaimsIdentity> GetIdentity(string email, string password)
        {
            var user = await _userService.GetUserByIdentity(email, password);

            if (user == null)
                return null;

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.UserType.ToString())
            };

            return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }

    /// <summary>
    /// auth interface
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// token response
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        Tuple<string, string> IdentityResponse(ClaimsIdentity identity);

        /// <summary>
        /// user claims
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<ClaimsIdentity> GetIdentity(string email, string password);
    }
}
