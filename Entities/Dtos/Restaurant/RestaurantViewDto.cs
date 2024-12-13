using Entities.Dtos.Item;
using Entities.Dtos.Review;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Restaurant
{
    public class RestaurantViewDto
    {
        public string Id { get; set; } = "";
        
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        public string Phone { get; set; } = "";

        public IEnumerable<ItemViewDto>? Menu { get; set; }
        public IEnumerable<ReviewViewDto>? Reviews { get; set; }

        public int Reviewcount => Reviews?.Count() ?? 0;
        
        public double AvarageRating => Reviews?.Count() > 0 ? Reviews.Average(r => r.Rating) : 0;
    }
}
