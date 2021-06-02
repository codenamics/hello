using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entity;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces {
    public interface IBoardRepository {

        void CreateBoard (Board board);
        Task<Board> GetBoardAsync (int boardId);
        Task<List<Board>> GetAllBoardsAsync ();
        Task<ActionResult> UpdateBoardAsync (int boardId);
        Task DeleteBoardAsync (int boardId);
        Task<bool> SaveChanges();
    }
}