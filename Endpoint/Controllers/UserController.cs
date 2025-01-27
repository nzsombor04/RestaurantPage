﻿using Data;
using Entities.Dtos.User;
using Entities.Models;
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
        UserManager<AppUser> userManager;
        RoleManager<IdentityRole> roleManager;
        DtoProvider dtoProvider;
        Repository<Restaurant> restaurantRepo;

        public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, DtoProvider dtoProvider, Repository<Restaurant> restaurantRepo)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.dtoProvider = dtoProvider;
            this.restaurantRepo = restaurantRepo;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
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

        [HttpGet("grantmanager/{username}")]
        [Authorize(Roles = "Admin")]
        public async Task GrantManager(string username, string restaurantid)
        {
            var role = await roleManager.RoleExistsAsync("Manager");
            if (!role)
            {
                await roleManager.CreateAsync(new IdentityRole("Manager"));
            }

            var user = await userManager.FindByNameAsync(username);
            if (user == null)
                throw new ArgumentException("User not found");

            var restaurant = restaurantRepo.FindById(restaurantid);

            if (restaurant == null)
                throw new ArgumentException("Restaurant not found");

            await userManager.AddToRoleAsync(user, "Manager");

            restaurant.ManagerId = user.Id;

            restaurantRepo.Update(restaurant);
        }

        [HttpPost("register")]
        public async Task Register(UserInputDto dto)
        {
            var existingUserEmail = await userManager.FindByEmailAsync(dto.Email);
            
            if (existingUserEmail != null)
            {
                throw new ArgumentException("Email already exists");
            }
            else
            {
                var existingUserName = await userManager.FindByNameAsync(dto.Username);

                if (existingUserName != null)
                {
                    throw new ArgumentException("Username already exists");
                }

                else
                {
                    var user = new AppUser(dto.Username);
                    user.Email = dto.Email;
                    user.FirstName = dto.FirstName;
                    user.LastName = dto.LastName;
                    await userManager.CreateAsync(user, dto.Password);
                    if (userManager.Users.Count() == 1)
                    {
                        await roleManager.CreateAsync(new IdentityRole("Admin"));
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                }
            }
        }

        [HttpGet("revokerole/{username}")]
        [Authorize(Roles = "Admin")]
        public async Task RevokeRole(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
                throw new ArgumentException("User not found");
            
            var isManager = await userManager.IsInRoleAsync(user, "Manager");

            if (isManager)
            {
                var restaurant = restaurantRepo.GetAll().Where(r => r.ManagerId == user.Id);

                foreach (var r in restaurant)
                {
                    r.ManagerId = "";
                    restaurantRepo.Update(r);
                }

                await userManager.RemoveFromRoleAsync(user, "Manager");
            }

            var roles = await userManager.GetRolesAsync(user);

            if (roles.Any())
            { 
                await userManager.RemoveFromRolesAsync(user, roles);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
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
