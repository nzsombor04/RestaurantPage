﻿using Entities.Dtos.Item;
using Logic.Logic;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        ItemLogic logic;

        public ItemController(ItemLogic logic)
        {
            this.logic = logic;
        }

        [HttpPost]
        public void AddItem(ItemCreateUpdateDto dto)
        {
            logic.AddItem(dto);
        }

        [HttpGet]
        public IEnumerable<ItemViewDto> GetAllItems()
        {
            return logic.GetAllItems();
        }

        [HttpDelete]
        public void DeleteItem(string id)
        {
            logic.DeleteItem(id);
        }

        [HttpPut]
        public void UpdateItem(string id, [FromBody] ItemCreateUpdateDto dto)
        {
            logic.UpdateItem(id, dto);
        }
    }
}