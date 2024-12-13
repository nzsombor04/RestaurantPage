using Entities.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Item : IIdEntity
    {
        public Item(string name, string description, int price)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Description = description;
            Price = price;
        }

        [Key]
        [StringLength(50)]
        public string Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [NotMapped]
        public virtual ICollection<Restaurant> Restaurants { get; set; }

        public int Price { get; set; }
    }
}
