using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO;
using API.Entity;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/item")]
    public class ItemController : ControllerBase
    {

        private readonly IListRepository _listRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IBoardRepository _boardRepository;
        private readonly IMapper _mapper;

        public ItemController(IListRepository listRepository, IItemRepository itemRepository, IBoardRepository boardRepository, IMapper mapper)
        {
            _boardRepository = boardRepository;
            _itemRepository = itemRepository;
            _listRepository = listRepository;
            _mapper = mapper;

        }

        [HttpPost("{id}")]
        public async Task<ActionResult> AddItemToList(Guid id, [FromBody] ItemDTO itemToAddDTO)
        {
            var list = await _listRepository.GetListAsync(id);
            if (list == null)
            {
                return NotFound();
            }
            var newItem = _mapper.Map<Item>(itemToAddDTO);

            if (list.Items.Count != 0)
            {

                foreach (var item in list.Items)
                {
                    item.Id = item.Id;
                    item.Order = item.Order + 1;
                }
                list.Items.Add(newItem);
            }
            else
            {
                list.Items.Add(newItem);
            }
            if (await _boardRepository.SaveChanges()) return Ok();

            return BadRequest("Failed to add item");
        }

        [HttpPut("updateItem")]
        public async Task<ActionResult> UpdateItem([FromBody] Item itemToUpdate)
        {
            _itemRepository.UpdateItem(itemToUpdate);
            if (await _boardRepository.SaveChanges()) return Ok();

            return BadRequest("Failed to update item");
        }

        [HttpPut("itemOrder/{id}")]
        public async Task<ActionResult> UpdateItemsOrderInList(Guid id, [FromBody] List<Item> items)
        {
            var list = await _listRepository.GetListAsync(id);

            if (list == null)
            {
                return NotFound();
            }

            var newListItemOrder = new AnnotateOrder<Item>().AnnotatedOrder(items);

            if (list.Items.Count != 0)
            {
                foreach (var newList in newListItemOrder)
                {
                    foreach (var oldItem in list.Items)
                    {
                        if (oldItem.Id == newList.Id)
                        {
                            oldItem.Id = oldItem.Id;
                            oldItem.Title = newList.Title;
                            oldItem.Order = newList.Order;
                        }
                    }

                }
            }
            _listRepository.UpdateList(list);
            if (await _boardRepository.SaveChanges()) return NoContent();

            return BadRequest("Failed to update items");
        }

        [HttpPut()]
        public async Task<ActionResult> UpdateItemOrderBetweenLists([FromBody] ListItemBetweenListsDTO items)
        {

            var list = new List<Item>();

            var newBoardListOrder = new AnnotateOrder<Item>().AnnotatedOrder(items.container.items);
            CreateNewItemList(list, newBoardListOrder, items.container.id);

            var listTwoAnn = new AnnotateOrder<Item>().AnnotatedOrder(items.previousContainer.items);
            CreateNewItemList(list, listTwoAnn, items.previousContainer.id);
            _itemRepository.UpdateItems(list);

            if (await _boardRepository.SaveChanges()) return Ok();

            return BadRequest("Failed to update list");
        }

        [HttpPut("{listId}/{itemId}")]
        public async Task<ActionResult> DeleteListItem(Guid listId, Guid itemId, [FromBody] List<Item> items)
        {
            var list = await _listRepository.GetListAsync(listId);
            var itemToRemove = await _itemRepository.GetItemAsync(itemId);
            if (list == null || itemToRemove == null)
            {
                return NotFound();
            }
            _itemRepository.DeleteItem(itemToRemove);

            var newListOrder = new AnnotateOrder<Item>().AnnotatedOrder(items);

            if (list.Items.Count != 0)
            {
                foreach (var newList in newListOrder)
                {
                    foreach (var oldItem in list.Items)
                    {
                        if (oldItem.Id == newList.Id)
                        {
                            oldItem.Id = oldItem.Id;
                            oldItem.Title = newList.Title;
                            oldItem.Order = newList.Order;
                        }
                    }

                }
            }
            _listRepository.UpdateList(list);
            if (await _boardRepository.SaveChanges()) return Ok();

            return BadRequest("Failed to delete list");
        }
        private List<Item> CreateNewItemList(List<Item> list, List<Item> items, Guid id)
        {
            foreach (var item in items)
            {
                list.Add(new Item
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    ListId = id,
                    Order = item.Order
                });
            }
            return list;
        }

    }
}