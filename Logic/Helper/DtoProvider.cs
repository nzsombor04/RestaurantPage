using AutoMapper;
using Data;
using Entities.Dtos.Item;
using Entities.Dtos.Restaurant;
using Entities.Dtos.Review;
using Entities.Dtos.User;
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

        UserManager<AppUser> userManager;
        public Mapper Mapper { get; }

        public DtoProvider(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Restaurant, RestaurantViewDto>().AfterMap((src, dest) =>
                {
                    var manager = userManager.Users.FirstOrDefault(u => u.Id == src.ManagerId);

                    if (manager == null)
                    {
                        dest.ManagerName = "";
                    }
                    else
                    {
                        dest.ManagerName = manager.LastName! + " " + manager.FirstName;
                    }
                }); ;

                cfg.CreateMap<Restaurant, RestaurantShortViewDto>()
                .AfterMap((src, dest) => 
                { 
                    dest.AvarageRating = src.Reviews?.Count() > 0 ? src.Reviews.Average(r => r.Rating) : 0;
                });

                cfg.CreateMap<AppUser, UserViewDto>()
                .AfterMap((src, dest) =>
                {
                    dest.IsAdmin = userManager.IsInRoleAsync(src, "Admin").Result;
                });

                cfg.CreateMap<RestaurantCreateUpdateDto, Restaurant>().ForMember(dest => dest.Menu, opt => opt.Ignore());
                cfg.CreateMap<Review, ReviewViewDto>()
                .AfterMap((src, dest) =>
                {
                    var user = userManager.Users.First(u => u.Id == src.UserId);
                    dest.UserFullName = user.LastName! + " " + user.FirstName;
                });
                cfg.CreateMap<ReviewCreateDto, Review>();
                cfg.CreateMap<ItemCreateUpdateDto, Item>();
                cfg.CreateMap<Item, ItemViewDto>();
            });

            Mapper = new Mapper(config);
        }
    }
}
