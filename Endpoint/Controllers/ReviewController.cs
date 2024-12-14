using Data;
using Entities.Dtos.Review;
using Logic.Logic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        ReviewLogic logic;
        UserManager<AppUser> userManager;


        public ReviewController(ReviewLogic logic, UserManager<AppUser> userManager)
        {
            this.logic = logic;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task AddRating(ReviewCreateDto dto)
        {
            var user = await userManager.GetUserAsync(User);

            logic.AddReview(dto, user.Id);
        }

        [HttpDelete]
        public void DeleteRating(string id)
        {
            logic.DeleteReview(id);
        }
    }
}
