using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Item
{
    public class ItemCreateUpdateDto
    {
        [StringLength(50)]
        public required string Name { get; set; }

        [StringLength(500)]
        public required string Description { get; set; }
        
        public required int Price { get; set; }

        public string RestaurantId { get; set; } = "";
    }
}
