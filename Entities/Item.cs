using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Item
    {
        public Item(string name, string description, int price)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Name = name;
            this.Description = description;
            this.Price = price;
        }

        [Key]
        [StringLength(50)]
        public string Id { get; set; } 

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int Price { get; set; }
    }
}
