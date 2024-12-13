using Entities.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Review : IIdEntity
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

        [NotMapped]
        public virtual Restaurant Restaurant { get; set; }
    }
}
