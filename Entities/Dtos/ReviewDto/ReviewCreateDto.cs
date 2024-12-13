using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.ReviewDto
{
    public class ReviewCreateDto
    {
        public required string RestaurantId { get; set; }

        [MinLength(5)]
        [MaxLength(500)]
        public required string Text { get; set; }

        [Range(1, 5)]
        public required int Rating { get; set; }
    }
}
