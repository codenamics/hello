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
        void UpdateBoard (Board board);
        void DeleteBoard (Board board);
        Task<bool> SaveChanges();
    }
}