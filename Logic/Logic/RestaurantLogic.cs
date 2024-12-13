using Data;
using Entities.Dtos.Restaurant;
using Entities.Models;
using Logic.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Logic
{
    public class RestaurantLogic
    {
        Repository<Restaurant> repo;
        DtoProvider dtoProvider;

        public RestaurantLogic(Repository<Restaurant> repo, DtoProvider dtoProvider)
        {
            this.repo = repo;
            this.dtoProvider = dtoProvider;
        }

        public void AddRestaurant(RestaurantCreateUpdateDto dto)
        {
            Restaurant r = dtoProvider.Mapper.Map<Restaurant>(dto);

            if (repo.GetAll().FirstOrDefault(x => x.Address == r.Address) == null)
            {
                repo.Create(r);
            }
            else
            {
                throw new ArgumentException("Az alábbi helyszínen már létezik étterem!");
            }
        }

        public IEnumerable<RestaurantShortViewDto> GetAllRestaurants()
        {
            return repo.GetAll().Select(x => dtoProvider.Mapper.Map<RestaurantShortViewDto>(x));
        }

        public void DeleteMovie(string id)
        {
            repo.DeleteById(id);
        }

        public void UpdateMovie(string id, RestaurantCreateUpdateDto dto)
        {
            var old = repo.FindById(id);
            dtoProvider.Mapper.Map(dto, old);
            repo.Update(old);
        }

        public RestaurantViewDto GetRestaurant(string id)
        {
            return dtoProvider.Mapper.Map<RestaurantViewDto>(repo.FindById(id));
        }
    }
}
