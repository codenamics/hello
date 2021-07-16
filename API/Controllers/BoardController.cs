using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO;
using API.Entity;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route ("api/board")]
    public class BoardController : ControllerBase {
        private readonly IBoardRepository _boardRepository;
        private readonly IMapper _mapper;

        public BoardController (IBoardRepository boardRepository, IMapper mapper) {
            _mapper = mapper;

            _boardRepository = boardRepository;
        }

        [HttpPost]
        public async Task<ActionResult> CreateBoard ([FromBody] BoardDTO boardCreationDTO) {
            var newBoard = _mapper.Map<Board>(boardCreationDTO);
            _boardRepository.CreateBoard (newBoard);
            if (await _boardRepository.SaveChanges ()) return Ok (newBoard);

            return BadRequest ("Failed to create board");
        }

        [HttpGet]
        public async Task<ActionResult<List<Board>>> GetAllBoards () {
            var boards = await _boardRepository.GetAllBoardsAsync ();
            if (boards == null) {
                return NotFound ();
            }
            return Ok (boards);
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<Board>> GetBoard (Guid id) {
            var board = await _boardRepository.GetBoardAsync (id);
            if (board == null) {
                return NotFound ();
            }
            return Ok (board);
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateBoard ([FromBody] BoardDTO boardUpdateDTO) {
            var updatedBoard = _mapper.Map<Board> (boardUpdateDTO);
            _boardRepository.UpdateBoard (updatedBoard);

            if (await _boardRepository.SaveChanges ()) return Ok ();

            return BadRequest ("Failed to update board");
        }

        [HttpDelete ("{id}")]
        public async Task<IActionResult> DeleteBoard (Guid id) {
            var board = await _boardRepository.GetBoardAsync (id);

            _boardRepository.DeleteBoard (board);

            if (await _boardRepository.SaveChanges ()) return Ok ();

            return BadRequest ("Failed to delete board");
        }
    }
}