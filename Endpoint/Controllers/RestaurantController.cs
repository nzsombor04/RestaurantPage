using Entities.Dtos.Restaurant;
using Logic.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        RestaurantLogic logic;

        public RestaurantController(RestaurantLogic logic)
        {
            this.logic = logic;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public void AddRestaurant(RestaurantCreateUpdateDto dto)
        { 
            logic.AddRestaurant(dto);
        }

        [HttpGet]
        public IEnumerable<RestaurantShortViewDto> GetAllRestaurants()
        {
            return logic.GetAllRestaurants();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public void DeleteRestaurant(string id)
        {
            logic.DeleteRestaurant(id);
        }

        [HttpPut("/UpdateRestaurant")]
        [Authorize(Roles = "Admin")]
        public void UpdateRestaurant(string id, [FromBody] RestaurantCreateUpdateDto dto)
        {
            logic.UpdateRestaurant(id, dto);
        }

        [HttpPut("/UpdateRestaurantMenu")]
        [Authorize(Roles = "Manager")]
        public void UpdateRestaurantMenu(string id, [FromBody] RestaurantMenuUpdateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId != logic.GetRestaurant(id).ManagerId)
            {
                throw new UnauthorizedAccessException("You are not the manager of this restaurant!");
            }

            logic.UpdateRestaurantMenu(id, dto);
        }

        [HttpGet("{id}")]
        public RestaurantViewDto GetRestaurant(string id)
        {
            return logic.GetRestaurant(id);
        }
    }
}
