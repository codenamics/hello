using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entity;

namespace API.Interfaces
{
    public interface IItemRepository
    {
          void UpdateItems (List<Item> item);
          void DeleteItem(Item item);
     Task<Item> GetItemAsync (Guid id);

    }
}