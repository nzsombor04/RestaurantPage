using Entities.Dtos.User;
using Logic.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserManager<IdentityUser> userManager;
        RoleManager<IdentityRole> roleManager;
        DtoProvider dtoProvider;

        public UserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, DtoProvider dtoProvider)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.dtoProvider = dtoProvider;
        }

        [HttpGet]
        public IEnumerable<UserViewDto> GetAllUsers()
        {
            return userManager.Users.Select(u => dtoProvider.Mapper.Map<UserViewDto>(u));
        }

        [HttpGet("grantadmin/{username}")]
        [Authorize(Roles = "Admin")]
        public async Task GrantAdmin(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
                throw new ArgumentException("User not found");
            await userManager.AddToRoleAsync(user, "Admin");
        }


        [HttpPost("register")]
        public async Task Register(UserInputDto dto)
        { 
            var existingUser = await userManager.FindByNameAsync(dto.Username);
            if (existingUser != null)
            {
                throw new ArgumentException("Username already exists");
            }
            else
            {
                var user = new IdentityUser(dto.Username);
                await userManager.CreateAsync(user, dto.Password);
                if (userManager.Users.Count() == 1)
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }

        [HttpGet("revokeadmin/{username}")]
        [Authorize(Roles = "Admin")]
        public async Task RevokeAdmin(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
                throw new ArgumentException("User not found");
            await userManager.RemoveFromRoleAsync(user, "Admin");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserInputDto dto)
        {
            var user = await userManager.FindByNameAsync(dto.Username);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }
            else
            {
                var result = await userManager.CheckPasswordAsync(user, dto.Password);
                if (!result)
                {
                    throw new ArgumentException("Incorrect password");
                }
                else
                {
                    var claim = new List<Claim>();
                    claim.Add(new Claim(ClaimTypes.Name, user.UserName!));
                    claim.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

                    foreach (var role in await userManager.GetRolesAsync(user))
                    {
                        claim.Add(new Claim(ClaimTypes.Role, role));
                    }

                    int expiryInMinutes = 24 * 60;
                    var token = GenerateAccessToken(claim, expiryInMinutes);
                    return Ok(new LoginResultDto()
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Expiration = DateTime.Now.AddMinutes(expiryInMinutes)
                    });
                }
            }

        }

        private JwtSecurityToken GenerateAccessToken(IEnumerable<Claim>? claims, int expiryInMinutes)
        {
            var signinKey = new SymmetricSecurityKey(
                  Encoding.UTF8.GetBytes("EzahosszukulcsamititkositEzahosszukulcsamititkositEzahosszukulcsamititkosit"));
            return new JwtSecurityToken(
                  issuer: "restaurant.com",
                  audience: "restaurant.com",
                  claims: claims?.ToArray(),
                  expires: DateTime.Now.AddMinutes(expiryInMinutes),
                  signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
                );
        }
    }
}
