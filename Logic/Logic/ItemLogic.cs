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

        public ItemLogic(Repository<Item> repo, DtoProvider dtoProvider)
        {
            this.repo = repo;
            this.dtoProvider = dtoProvider;
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
