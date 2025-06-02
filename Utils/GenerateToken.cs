using Microsoft.IdentityModel.Tokens;
using OrgManager_API.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrgManager_API.Utils
{
    public static class GenerateToken
    {
        public static JwtSecurityToken GenerateNewToken(ApplicationUser user, int expireTimeInHours, IConfiguration config)
        {
            // Generate the JWT Token
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
                // Add more claims if needed
            };

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
