using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entity;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Data {
    public class BoardRepository : IBoardRepository {
        private ApplicationDBContext _context;
        public BoardRepository (ApplicationDBContext context) {
            _context = context;
        }

        public void CreateBoard (Board board) {
            _context.Boards.Add (board);
        }
        public async Task DeleteBoardAsync (int boardId) {
            var board = await _context.Boards.Include (x => x.Lists)
                .ThenInclude (y => y.Items).FirstOrDefaultAsync (x => x.Id == boardId);
            if (board != null) {
                _context.Boards.Remove (board);
            } else {
                return;
            }
        }
        public Task<List<Board>> GetAllBoardsAsync () {
            return _context.Boards.Include (x => x.Lists).ThenInclude (x => x.Items).ToListAsync ();
        }
        public async Task<Board> GetBoardAsync (int boardId) {
            return await _context.Boards.Include (x => x.Lists).ThenInclude (x => x.Items).FirstOrDefaultAsync (x => x.Id == boardId);
        }
        public async Task<bool> SaveChanges () {
            return await _context.SaveChangesAsync () > 0;

        }
        public Task<ActionResult> UpdateBoardAsync (int boardId) {
            throw new System.NotImplementedException ();
        }
    }
}