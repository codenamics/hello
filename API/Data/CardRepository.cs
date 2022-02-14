using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entity;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace API.Data {
    public class CardRepository : ICardRepository {
        private ApplicationDBContext _context;
        public CardRepository (ApplicationDBContext context) {
            _context = context;
        }

        public void UpdateCards (List<Card> card) {
            _context.Cards.UpdateRange (card);
        }
        public void DeleteCard (Card card) {
            _context.Entry (card).State = EntityState.Deleted;
        }
        public async Task<Card> GetCardAsync (Guid id) {
            return await _context.Cards.FirstOrDefaultAsync (x => x.Id == id);
        }

        public void UpdateCard (Card card) {
            _context.Entry (card).State = EntityState.Modified;
        }

       

    }
}