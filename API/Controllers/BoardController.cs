using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entity;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route ("api/boards")]
    public class BoardController : ControllerBase {
        private readonly IBoardRepository _boardRepository;

        public BoardController (IBoardRepository boardRepository) {

            _boardRepository = boardRepository;
        }

        [HttpPost]
        public async Task<ActionResult> CreateBoard (Board board) {
            _boardRepository.CreateBoard (board);
            if (await _boardRepository.SaveChanges ()) return NoContent ();

            return BadRequest ("Failed to create board");
        }
        // [HttpPost("{id}")]
        // public async Task<ActionResult> add (int id,[FromBody] List list) {
        //   var board = await _boardRepository.GetBoardAsync(id);
        //     board.Lists.Add(list);
        //     if (await _boardRepository.SaveChanges ()) return Ok();

        //     return BadRequest ("Failed to create board");
        // }
        [HttpGet]
        public async Task<ActionResult<List<Board>>> GetAllBoards () {
          var boards = await _boardRepository.GetAllBoardsAsync();
            if(boards == null){
                return BadRequest ("Failed to get boards");
             
            }
               return Ok(boards);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateBoard ([FromBody]Board board) {
           
            _boardRepository.UpdateBoard(board);

            if (await _boardRepository.SaveChanges ()) return Ok();

            return BadRequest ("Failed to update board");
        }
         [HttpDelete]
        public async Task<ActionResult> DeleteBoard ([FromBody]Board board) {
           
            _boardRepository.DeleteBoard(board);

            if (await _boardRepository.SaveChanges ()) return Ok();

            return BadRequest ("Failed to delete board");
        }
    }
}