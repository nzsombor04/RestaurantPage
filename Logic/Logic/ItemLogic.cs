using Data;
using Entities.Dtos.Item;
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
    public class ItemLogic
    {
        Repository<Item> repo;
        DtoProvider dtoProvider;
        Repository<Restaurant> restaurantRepo;

        public ItemLogic(Repository<Item> repo, DtoProvider dtoProvider, Repository<Restaurant> restaurantRepo)
        {
            this.repo = repo;
            this.dtoProvider = dtoProvider;
            this.restaurantRepo = restaurantRepo;
        }

        public IEnumerable<ItemViewDto> GetAllItems()
        {
            return repo.GetAll().Select(x => dtoProvider.Mapper.Map<ItemViewDto>(x));
        }

        public void AddItem(ItemCreateUpdateDto dto)
        {
            Item i = dtoProvider.Mapper.Map<Item>(dto);

            if (repo.GetAll().FirstOrDefault(x => x.Name == i.Name) == null)
            {
                i.Restaurants = restaurantRepo.GetAll() 
                    .Where(x => dto.RestaurantId.Contains(x.Id))
                    .ToList();

                repo.Create(i);
            }
            else
            {
                throw new ArgumentException("Az alábbi néven már létezik item!");
            }
        }

        public void DeleteItem(string id)
        {
            repo.DeleteById(id);
        }

        public void UpdateItem(string id, ItemCreateUpdateDto dto)
        {
            var old = repo.FindById(id);
            dtoProvider.Mapper.Map(dto, old);
            repo.Update(old);
        }
    }
}
