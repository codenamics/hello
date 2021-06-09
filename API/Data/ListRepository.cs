using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entity;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Data {
    public class ListRepository : IListRepository {
        private readonly IBoardRepository _boardRepository;
        private readonly ApplicationDBContext _context;

        public ListRepository (ApplicationDBContext context) {
            _context = context;
        }

        public void DeleteList (List list) {
            _context.Entry (list).State = EntityState.Deleted;
        }

        public async Task<List> GetListAsync (Guid id) {
            return await _context.Lists.Include (x => x.Items).FirstOrDefaultAsync (x => x.Id == id);
        }

        public void UpdateList (List list) {

            _context.Entry (list).State = EntityState.Modified;
            _context.Entry (list).Property (x => x.BoardId).IsModified = false;

        }

    }
}