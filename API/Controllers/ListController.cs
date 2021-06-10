using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using API.Entity;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/list")]
    public class ListController : ControllerBase
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IListRepository _listRepository;
        private readonly IMapper _mapper;

        public ListController(IBoardRepository boardRepository, IListRepository listRepository, IMapper mapper)
        {
            _mapper = mapper;
            _listRepository = listRepository;
            _boardRepository = boardRepository;
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> CreateList(Guid id, [FromBody] List list)
        {
      
            var board = await _boardRepository.GetBoardAsync(id);

            if(board.Lists.Count != 0){
              
                foreach (var item in board.Lists)
                {
                    item.Id = item.Id;
                    item.Order = item.Order + 1;
                }
                board.Lists.Add(new List{
                    Id = list.Id,
                    Title = list.Title
                });
            }else{
                 board.Lists.Add(new List{
                    Id = list.Id,
                    Title = list.Title
                });
            }
            if (await _boardRepository.SaveChanges()) return Ok();

            return BadRequest("Cannot add into board");

        }

        [HttpPut]
        public async Task<ActionResult> UpdateList([FromBody] List list)
        {

            _listRepository.UpdateList(list);

            if (await _boardRepository.SaveChanges()) return NoContent();

            return BadRequest("Failed to update list");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateListOrder(Guid id, [FromBody] List<List> list)
        {
            var board = await _boardRepository.GetBoardAsync(id);

            var newBoardListOrder = new AnnotateOrder<List>().AnnotatedOrder(list);
            

            if (board.Lists.Count != 0)
            {
                foreach (var newList in newBoardListOrder)
                {
                    foreach (var oldList in board.Lists)
                    {
                        if (oldList.Id == newList.Id)
                        {
                            oldList.Id = oldList.Id;
                            oldList.Title = newList.Title;
                            oldList.Order = newList.Order;
                        }
                    }

                }
            }
            _boardRepository.UpdateBoard(board);
            if (await _boardRepository.SaveChanges()) return NoContent();

            return BadRequest("Failed to update list");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetListById(Guid id)
        {
            var list = await _listRepository.GetListAsync(id);

            return Ok(list);
        }

        [HttpDelete("{boardId}/{listId}")]
        public async Task<ActionResult> DeleteList(Guid boardId, Guid listId, [FromBody] List<List> lists)
        {
            var board = await _boardRepository.GetBoardAsync(boardId);
            var listToRemove = await _listRepository.GetListAsync(listId);
            
            _listRepository.DeleteList(listToRemove);
           
            var newBoardListOrder = new AnnotateOrder<List>().AnnotatedOrder(lists);

            if (board.Lists.Count != 0)
            {   
                
                foreach (var newList in newBoardListOrder)
                {
                    foreach (var oldList in board.Lists)
                    {
                        if (oldList.Id == newList.Id)
                        {
                            oldList.Id = oldList.Id;
                            oldList.Title = newList.Title;
                            oldList.Order = newList.Order;
                        }
                    }

                }
            }
            _boardRepository.UpdateBoard(board);
            if (await _boardRepository.SaveChanges()) return Ok();
           
            return BadRequest("Failed to delete list");
        }
    

    }
}