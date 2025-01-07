using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Restaurant
{
    public class RestaurantMenuUpdateDto
    {
        public List<string> Menu { get; set; } = new List<string>();
    }
}
