using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entity;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route ("api/item")]
    public class ItemController : ControllerBase {

        private readonly IMapper _mapper;
        private readonly IListRepository _listRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IBoardRepository _boardRepository;

        public ItemController (IListRepository listRepository, IItemRepository itemRepository, IBoardRepository boardRepository) {
            _boardRepository = boardRepository;
            _itemRepository = itemRepository;
            _listRepository = listRepository;

        }

        [HttpPost ("{id}")]
        public async Task<ActionResult> AddItemToList (Guid id, [FromBody] Item itemToAdd) {
            var list = await _listRepository.GetListAsync (id);

            if (list.Items.Count != 0) {

                foreach (var item in list.Items) {
                    item.Id = item.Id;
                    item.Order = item.Order + 1;
                }
                list.Items.Add (new Item {
                    Id = itemToAdd.Id,
                        Title = itemToAdd.Title
                });
            } else {
                list.Items.Add (new Item {
                    Id = itemToAdd.Id,
                        Title = itemToAdd.Title
                });
            }
            if (await _boardRepository.SaveChanges ()) return Ok ();

            return BadRequest ("Failed to add item");
        }

        [HttpPut ("itemOrder/{id}")]
        public async Task<ActionResult> UpdateItemsOrderInList (Guid id, [FromBody] List<Item> items) {
            var list = await _listRepository.GetListAsync (id);

            if (list == null) {
                return NotFound ();
            }

            var newListItemOrder = new AnnotateOrder<Item> ().AnnotatedOrder (items);

            if (list.Items.Count != 0) {
                foreach (var newList in newListItemOrder) {
                    foreach (var oldItem in list.Items) {
                        if (oldItem.Id == newList.Id) {
                            oldItem.Id = oldItem.Id;
                            oldItem.Title = newList.Title;
                            oldItem.Order = newList.Order;
                        }
                    }

                }
            }
            _listRepository.UpdateList (list);
            if (await _boardRepository.SaveChanges ()) return NoContent ();

            return BadRequest ("Failed to update items");
        }

        [HttpPut ()]
        public async Task<ActionResult> UpdateItemOrderBetweenLists ([FromBody] ListItemBetweenListsDTO items) {

            var list = new List<Item> ();

            var newBoardListOrder = new AnnotateOrder<Item> ().AnnotatedOrder (items.previous.items);
            CreateNewItemList (list, newBoardListOrder, items.previous.id);

            var listTwoAnn = new AnnotateOrder<Item> ().AnnotatedOrder (items.current.items);
            CreateNewItemList (list, listTwoAnn, items.current.id);
            _itemRepository.UpdateItems (list);

            if (await _boardRepository.SaveChanges ()) return Ok ();

            return BadRequest ("Failed to update list");
        }

        [HttpDelete ("{listId}/{itemId}")]
        public async Task<ActionResult> DeleteList (Guid listId, Guid itemId, [FromBody] List<Item> items) {
            var list = await _listRepository.GetListAsync (listId);
            var itemToRemove = await _itemRepository.GetItemAsync (itemId);

            _itemRepository.DeleteItem (itemToRemove);

            var newListOrder = new AnnotateOrder<Item> ().AnnotatedOrder (items);

            if (list.Items.Count != 0) {
                foreach (var newList in newListOrder) {
                    foreach (var oldItem in list.Items) {
                        if (oldItem.Id == newList.Id) {
                            oldItem.Id = oldItem.Id;
                            oldItem.Title = newList.Title;
                            oldItem.Order = newList.Order;
                        }
                    }

                }
            }
            _listRepository.UpdateList (list);
            if (await _boardRepository.SaveChanges ()) return Ok ();

            return BadRequest ("Failed to delete list");
        }
        private List<Item> CreateNewItemList (List<Item> list, List<Item> items, Guid id) {
            foreach (var item in items) {
                list.Add (new Item {
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