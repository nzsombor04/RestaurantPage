using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Restaurant
    {
        public Restaurant(string name, string address, string phone, ICollection<Item> menu, ICollection<Review> reviews)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Address = address;
            Phone = phone;
            Menu = menu;
            Reviews = reviews;
        }

        [Key]
        [StringLength(50)]
        public string Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        public virtual ICollection<Item> Menu { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
