using System.Threading.Tasks;
using API.Entity;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ListRepository : IListRepository
    {
        private readonly IBoardRepository _boardRepository;
        private readonly ApplicationDBContext _context;

        public ListRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public void DeleteList(List list)
        {
            _context.Lists.Remove(list);
        }

        public async Task<List> GetListAsync(int id)
        {
            return await _context.Lists.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void UpdateList(List list)
        {
            _context.Entry(list).State = EntityState.Modified;
        } 
    }
}