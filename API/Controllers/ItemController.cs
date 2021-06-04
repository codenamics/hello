using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Entity;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/items")]
    public class ItemController : ControllerBase
    {

        private readonly IListRepository _listRepository;
        private readonly IMapper _mapper;
        private readonly IBoardRepository _boardRepository;

        public ItemController(IBoardRepository boardRepository, IListRepository listRepository, IMapper mapper)
        {
            _boardRepository = boardRepository;
            _mapper = mapper;
            _listRepository = listRepository;

        }

        [HttpPost("{id}")]
        public async Task<ActionResult> AddItemToList(int id, [FromBody] Item item)
        {
            var list = await _listRepository.GetListAsync(id);
            list.Items.Add(item);

            if (await _boardRepository.SaveChanges()) return Ok();

            return BadRequest("Failed to add item");
        }
        // [HttpPut]
        // public async Task<ActionResult> UpdateList([FromBody] List list)
        // {

        //     _listRepository.UpdateList(list);

        //     if (await _boardRepository.SaveChanges()) return NoContent();

        //     return BadRequest("Failed to update list");
        // }
        // [HttpPut("{id}")]
        // public async Task<ActionResult> UpdateListOrder(int id, [FromBody] List<List> list)
        // {
        //     var board = await _boardRepository.GetBoardAsync(id);

        //     var newBoardListOrder = AnnotateOrder(list);

        //     if (board.Lists != null)
        //     {
        //         foreach (var newList in newBoardListOrder)
        //         {
        //             foreach (var oldList in board.Lists)
        //             {
        //                 if (oldList.Id == newList.Id)
        //                 {
        //                     oldList.Id = oldList.Id;
        //                     oldList.Title = newList.Title;
        //                     oldList.Order = newList.Order;
        //                 }
        //             }

        //         }
        //     }
        //     _boardRepository.UpdateBoard(board);
        //     if (await _boardRepository.SaveChanges()) return NoContent();

        //     return BadRequest("Failed to update list");
        // }
        // [HttpDelete]
        // public async Task<ActionResult> DeleteList([FromBody] List list)
        // {

        //     _listRepository.DeleteList(list);

        //     if (await _boardRepository.SaveChanges()) return NoContent();

        //     return BadRequest("Failed to delete list");
        // }
        // private List<List> AnnotateOrder(List<List> list)
        // {

        //     if (list != null)
        //     {
        //         for (int i = 0; i < list.Count; i++)
        //         {

        //             list.ToArray()[i].Order = i;
        //         }
        //     }
        //     return list;
        // }

    }
}