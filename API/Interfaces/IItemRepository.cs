using System.Collections.Generic;
using API.Entity;

namespace API.Interfaces
{
    public interface IItemRepository
    {
          void UpdateItems (List<Item> item);
    }
}