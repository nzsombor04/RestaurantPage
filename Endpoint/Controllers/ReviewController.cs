using Entities.Dtos.Review;
using Logic.Logic;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        ReviewLogic logic;

        public ReviewController(ReviewLogic logic)
        {
            this.logic = logic;
        }

        [HttpPost]
        public void AddRating(ReviewCreateDto dto)
        {
            logic.AddReview(dto);
        }

        [HttpDelete]
        public void DeleteRating(string id)
        {
            logic.DeleteReview(id);
        }
    }
}
