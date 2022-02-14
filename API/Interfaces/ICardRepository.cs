using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entity;

namespace API.Interfaces
{
    public interface ICardRepository
    {
          void UpdateCards (List<Card> card);
          void UpdateCard (Card card);
          void DeleteCard(Card card);
     Task<Card> GetCardAsync (Guid id);

    }
}