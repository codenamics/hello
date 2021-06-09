using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Entity;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Index.HPRtree;

namespace API.Controllers
{
    [Route("api/item")]
    public class ItemController : ControllerBase
    {


        private readonly IMapper _mapper;
        private readonly IBoardRepository _boardRepository;
        private readonly IItemRepository _itemRepository;

        public ItemController(IBoardRepository boardRepository, IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
            _boardRepository = boardRepository;



        }

        [HttpPost("{id}")]
        public async Task<ActionResult> AddItemToList(Guid id, [FromBody] List<Item> item)
        {
            var list = new List<Item>();
            var listOneAnn = AnnotateOrder(item);
            foreach (var items in listOneAnn.ToList())
            {
                list.Add(new Item
                {
                    Id = items.Id,
                    Title = items.Title,
                    Description = items.Description,
                    ListId = id,
                    Order = items.Order
                });

            }
            // _itemRepository.UpdateItems(list);
            // if (await _boardRepository.SaveChanges()) 
            return Ok(list);

            return BadRequest("Failed to add item");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemOrderInList(Guid id, [FromBody] Root items)
        {

            var list = new List<Item>();
            var listOneAnn = AnnotateOrder(items.previous.items);
            foreach (var item in listOneAnn)
            {
                list.Add(new Item
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    ListId = items.previous.id,
                    Order = item.Order
                });

            }

            var listTwoAnn = AnnotateOrder(items.current.items);
            foreach (var item in listTwoAnn)
            {
                list.Add(new Item
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    ListId = items.current.id,
                    Order = item.Order
                });
            }

            _itemRepository.UpdateItems(list);

            if (await _boardRepository.SaveChanges()) return Ok();

            return BadRequest("Failed to update list");
        }
        private List<Item> AnnotateOrder(List<Item> list)
        {

            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {

                    list[i].Order = i;
                }
            }
            return list;
        }

    }
}