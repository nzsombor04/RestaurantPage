using AutoMapper;
using Entities.Dtos.Item;
using Entities.Dtos.Restaurant;
using Entities.Dtos.Review;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Helper
{
    public class DtoProvider
    {

        UserManager<IdentityUser> userManager;
        public Mapper Mapper { get; }

        public DtoProvider(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Restaurant, RestaurantViewDto>();

                cfg.CreateMap<Restaurant, RestaurantShortViewDto>()
                .AfterMap((src, dest) => 
                { 
                    dest.AvarageRating = src.Reviews?.Count() > 0 ? src.Reviews.Average(r => r.Rating) : 0;
                });

                cfg.CreateMap<RestaurantCreateUpdateDto, Restaurant>();
                cfg.CreateMap<Review, ReviewViewDto>()
                .AfterMap((src, dest) =>
                { 
                    dest.UserName = userManager.Users.First(u => u.Id == src.UserId).UserName!;
                });
                cfg.CreateMap<ReviewCreateDto, Review>();
                cfg.CreateMap<ItemCreateUpdateDto, Item>();
                cfg.CreateMap<Item, ItemViewDto>();
            });

            Mapper = new Mapper(config);
        }
    }
}
