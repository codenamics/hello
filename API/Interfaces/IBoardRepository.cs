using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO;
using API.Entity;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces {
    public interface IBoardRepository {

        void CreateBoard (Board board);
        void UpdateBoard (Board board);
        void DeleteBoard (Board board);
        Task<Board> GetBoardAsync (Guid boardId);
        Task<List<Board>> GetAllBoardsAsync ();
        Task<bool> SaveChanges();
    }
}