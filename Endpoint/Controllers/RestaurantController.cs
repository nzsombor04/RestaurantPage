using Entities.Dtos.Restaurant;
using Logic.Logic;
using Microsoft.AspNetCore.Mvc;

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
        public void DeleteRestaurant(string id)
        {
            logic.DeleteRestaurant(id);
        }

        [HttpPut]
        public void UpdateRestaurant(string id, [FromBody] RestaurantCreateUpdateDto dto)
        {
            logic.UpdateRestaurant(id, dto);
        }

        [HttpGet("{id}")]
        public RestaurantViewDto GetRestaurant(string id)
        {
            return logic.GetRestaurant(id);
        }
    }
}
