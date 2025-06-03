using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OrgManager_API.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrgManager_API.Utils
{
    public static class GenerateToken
    {
        public static async Task<JwtSecurityToken> GenerateNewToken(
            ApplicationUser user,
            int expireTimeInHours,
            IConfiguration config,
            UserManager<ApplicationUser> userManager)
        {
            // Basic claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            // Add role claims
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var mytoken = new JwtSecurityToken(
                issuer: config["JWT:Issuer"],
                audience: config["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(expireTimeInHours),
                signingCredentials: creds
            );
            return mytoken;
        }
    }
}
