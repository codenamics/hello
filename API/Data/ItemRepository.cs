using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entity;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Index.HPRtree;

namespace API.Data {
    public class ItemRepository : IItemRepository {
        private ApplicationDBContext _context;
        public ItemRepository (ApplicationDBContext context) {
            _context = context;
        }

        public void UpdateItems (List<Item> item) {
            _context.Items.UpdateRange (item);
        }
        public void DeleteItem (Item item) {
            _context.Entry (item).State = EntityState.Deleted;
        }
        public async Task<Item> GetItemAsync (Guid id) {
            return await _context.Items.FirstOrDefaultAsync (x => x.Id == id);
        }

        public void UpdateItem (Item item) {
            _context.Entry (item).State = EntityState.Modified;
        }
    }
}