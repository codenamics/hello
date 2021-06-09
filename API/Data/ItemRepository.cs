using System.Collections.Generic;
using API.Entity;
using API.Interfaces;

namespace API.Data {
    public class ItemRepository : IItemRepository {
        private ApplicationDBContext _context;
        public ItemRepository (ApplicationDBContext context) {
            _context = context;
        }

        public void UpdateItems (List<Item> item) {
            _context.Items.UpdateRange (item);
        }

    }
}