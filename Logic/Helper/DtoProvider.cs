using AutoMapper;
using Entities.Dtos.Item;
using Entities.Dtos.Restaurant;
using Entities.Dtos.Review;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Helper
{
    public class DtoProvider
    {
        public Mapper Mapper { get; }

        public DtoProvider()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Restaurant, RestaurantViewDto>();
                cfg.CreateMap<Restaurant, RestaurantShortViewDto>();
                cfg.CreateMap<RestaurantCreateUpdateDto, Restaurant>();
                cfg.CreateMap<Review, ReviewViewDto>();
                cfg.CreateMap<ReviewCreateDto, Review>();
                cfg.CreateMap<ItemCreateUpdateDto, Item>();
                cfg.CreateMap<Item, ItemViewDto>();
            });

            Mapper = new Mapper(config);
        }
    }
}
