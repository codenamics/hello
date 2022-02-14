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
    [Route("api/card")]
    public class CardController : ControllerBase
    {

        private readonly IListRepository _listRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IBoardRepository _boardRepository;
        private readonly IMapper _mapper;

        public CardController(IListRepository listRepository, ICardRepository cardRepository, IBoardRepository boardRepository, IMapper mapper)
        {
            _boardRepository = boardRepository;
            _cardRepository = cardRepository;
            _listRepository = listRepository;
            _mapper = mapper;

        }

        [HttpPost("{id}")]
        public async Task<ActionResult> AddCardToList(Guid id, [FromBody] CardDTO cardToAddDTO)
        {
            var list = await _listRepository.GetListAsync(id);
            if (list == null)
            {
                return NotFound();
            }
            var newCard = _mapper.Map<Card>(cardToAddDTO);

            if (list.Cards.Count != 0)
            {

                foreach (var item in list.Cards)
                {
                    item.Id = item.Id;
                    item.Order = item.Order + 1;
                }
                list.Cards.Add(newCard);
            }
            else
            {
                list.Cards.Add(newCard);
            }
            if (await _boardRepository.SaveChanges()) return Ok();

            return BadRequest("Failed to add item");
        }

        [HttpPut("updateItem")]
        public async Task<ActionResult> UpdateItem([FromBody] Card cardToUpdate)
        {
            _cardRepository.UpdateCard(cardToUpdate);
            if (await _boardRepository.SaveChanges()) return Ok();

            return BadRequest("Failed to update item");
        }

        [HttpPut("itemOrder/{id}")]
        public async Task<ActionResult> UpdateCardOrderInList(Guid id, [FromBody] List<Card> cards)
        {
            var list = await _listRepository.GetListAsync(id);

            if (list == null)
            {
                return NotFound();
            }

            var newListItemOrder = new AnnotateOrder<Card>().AnnotatedOrder(cards);

            if (list.Cards.Count != 0)
            {
                foreach (var newList in newListItemOrder)
                {
                    foreach (var oldItem in list.Cards)
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
        public async Task<ActionResult> UpdateItemOrderBetweenLists([FromBody] ListCardBetweenListsDTO items)
        {

            var list = new List<Card>();

            var newBoardListOrder = new AnnotateOrder<Card>().AnnotatedOrder(items.container.cards);
            CreateNewItemList(list, newBoardListOrder, items.container.id);

            var listTwoAnn = new AnnotateOrder<Card>().AnnotatedOrder(items.previousContainer.cards);
            CreateNewItemList(list, listTwoAnn, items.previousContainer.id);
            _cardRepository.UpdateCards(list);

            if (await _boardRepository.SaveChanges()) return Ok();

            return BadRequest("Failed to update list");
        }

        [HttpPut("{listId}/{itemId}")]
        public async Task<ActionResult> DeleteListItem(Guid listId, Guid itemId, [FromBody] List<Card> items)
        {
            var list = await _listRepository.GetListAsync(listId);
            var itemToRemove = await _cardRepository.GetCardAsync(itemId);
            if (list == null || itemToRemove == null)
            {
                return NotFound();
            }
            _cardRepository.DeleteCard(itemToRemove);

            var newListOrder = new AnnotateOrder<Card>().AnnotatedOrder(items);

            if (list.Cards.Count != 0)
            {
                foreach (var newList in newListOrder)
                {
                    foreach (var oldItem in list.Cards)
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
        private List<Card> CreateNewItemList(List<Card> list, List<Card> items, Guid id)
        {
            foreach (var item in items)
            {
                list.Add(new Card
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