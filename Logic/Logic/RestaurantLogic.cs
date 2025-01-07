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
        Repository<Item> itemRepo;
        DtoProvider dtoProvider;

        public RestaurantLogic(Repository<Restaurant> repo, DtoProvider dtoProvider, Repository<Item> itemRepo)
        {
            this.repo = repo;
            this.dtoProvider = dtoProvider;
            this.itemRepo = itemRepo;
        }

        public void AddRestaurant(RestaurantCreateUpdateDto dto)
        {
            Restaurant r = dtoProvider.Mapper.Map<Restaurant>(dto);

            r.Menu = dto.Menu.Select(itemId => itemRepo.FindById(itemId)).ToList();

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

        public void DeleteRestaurant(string id)
        {
            repo.DeleteById(id);
        }

        public void UpdateRestaurant(string id, RestaurantCreateUpdateDto dto)
        {
            var old = repo.FindById(id);

            old.Menu.Clear();

            old.Menu = dto.Menu.Select(itemId => itemRepo.FindById(itemId)).ToList();
            
            dtoProvider.Mapper.Map(dto, old);
            repo.Update(old);
        }

        public RestaurantViewDto GetRestaurant(string id)
        {
            return dtoProvider.Mapper.Map<RestaurantViewDto>(repo.FindById(id));
        }
    }
}
