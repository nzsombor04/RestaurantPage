using Entities.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
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

        [Key]
        [StringLength(50)]
        public string Id { get; set; }

        [StringLength(50)]
        public string RestaurantId { get; set; }

        [StringLength(500)]
        public string Text { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(50)]
        public string UserId { get; set; }

        [NotMapped]
        public virtual Restaurant Restaurant { get; set; }
    }
}
