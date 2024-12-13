using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Restaurant
{
    public class RestaurantCreateUpdateDto
    {
        public required string Name { get; set; } = "";
        public required string Address { get; set; } = "";
        public required string Phone { get; set; } = "";
    }
}
