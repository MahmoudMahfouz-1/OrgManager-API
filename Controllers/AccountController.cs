using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OrgManager_API.DTO;
using OrgManager_API.Model;

namespace OrgManager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;

        public IConfiguration Config { get; }

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            this.userManager = userManager;
            Config = config;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserDto userDto)
        {
            if (userDto == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if the username is already taken
            var existingUserByName = await userManager.FindByNameAsync(userDto.UserName);
            if (existingUserByName != null)
            {
                ModelState.AddModelError("UserName", "Username is already used");
                return BadRequest(ModelState);
            }

            // Check if the email is already taken
            var existingUserByEmail = await userManager.FindByEmailAsync(userDto.Email);
            if (existingUserByEmail != null)
            {
                ModelState.AddModelError("Email", "Email is already used");
                return BadRequest(ModelState);
            }

            // Create the user
            var user = new ApplicationUser
            {
                UserName = userDto.UserName,
                Email = userDto.Email
                // Add other properties if needed
            };

            var result = await userManager.CreateAsync(user, userDto.Password);

            if (result.Succeeded)
            {
                return Ok("Account created successfully");
            }

            // Add identity errors to ModelState
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDto userDto)
        {
            if (ModelState.IsValid && userDto != null)
            {
                ApplicationUser user = await userManager.FindByNameAsync(userDto.UserName);
                if (user != null)
                {
                    bool found = await userManager.CheckPasswordAsync(user, userDto.Password);
                    if (found)
                    {
                        // Generate the JWT Token
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.NameIdentifier, user.Id)
                            // Add more claims if needed
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["JWT:Key"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var mytoken = new JwtSecurityToken(
                            issuer: Config["JWT:Issuer"],
                            audience: Config["JWT:Audience"],
                            claims: claims,
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: creds
                        );

                        var tokenString = new JwtSecurityTokenHandler().WriteToken(mytoken);

                        return Ok(new { user, token = tokenString, validTo = mytoken.ValidTo });
                    }
                }
            }
            return Unauthorized();
        }
    }
}
