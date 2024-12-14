using Entities.Dtos.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Endpoint.Controllers
{
    public class UserController : ControllerBase
    {
        UserManager<IdentityUser> userManager;

        public UserController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task Register(UserCreateDto dto)
        {
            var user = new IdentityUser(dto.Username);
            await userManager.CreateAsync(user, dto.Password);
        }
    }
}
