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
using OrgManager_API.Utils;

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
                var mytoken = GenerateToken.GenerateNewToken(user, 1, Config);
                var tokenString = new JwtSecurityTokenHandler().WriteToken(mytoken);

                return Ok(new { user, token = tokenString, validTo = mytoken.ValidTo });
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
            if (userDto == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userManager.FindByNameAsync(userDto.UserName);
            if (user == null)
                return Unauthorized(new { message = "Invalid username or password." });

            var passwordValid = await userManager.CheckPasswordAsync(user, userDto.Password);
            if (!passwordValid)
                return Unauthorized(new { message = "Invalid username or password." });

            var token = GenerateToken.GenerateNewToken(user, 1, Config);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            // Return only safe user info
            return Ok(new
            {
                user = new { user.Id, user.UserName, user.Email },
                token = tokenString,
                validTo = token.ValidTo
            });
        }
    }
}

