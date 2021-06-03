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
        public void DeleteBoard (Board board) {
 
           
                _context.Boards.Remove (board);
           
        }
        public Task<List<Board>> GetAllBoardsAsync () {
            return _context.Boards.Include (x => x.Lists).ThenInclude (x => x.Items).AsSplitQuery().ToListAsync ();
        }
        public async Task<Board> GetBoardAsync (int boardId) {
            return await _context.Boards.Include (x => x.Lists).ThenInclude (x => x.Items).AsSplitQuery().FirstOrDefaultAsync (x => x.Id == boardId);
        }
        public async Task<bool> SaveChanges () {
            return await _context.SaveChangesAsync () > 0;

        }
        public void UpdateBoard(Board board) {
             _context.Entry(board).State = EntityState.Modified;
        }
    }
}