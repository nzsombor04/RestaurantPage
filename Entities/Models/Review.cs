using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Review
    {
        public Review(string restaurantId, string text, int rating)
        {
            this.Id = Guid.NewGuid().ToString();
            this.RestaurantId = restaurantId;
            this.Text = text;
            this.Rating = rating;
        }

        public string Id { get; set; }
        public string RestaurantId { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
    }
}
