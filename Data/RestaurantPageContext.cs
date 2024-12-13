using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class RestaurantPageContext : IdentityDbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public RestaurantPageContext(DbContextOptions<RestaurantPageContext> ctx) : base(ctx)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Restaurant>()
                .HasMany(r => r.Menu)
                .WithOne(i => i.Restaurant)
                .HasForeignKey(i => i.RestaurantId);

            base.OnModelCreating(builder);
        }
    }
}
