using System;
using System.Collections.Generic;

namespace API.Entity
{
    public class ListItemDTO
    {
        public Guid id { get; set; }
        public List<Item> items { get; set; }
    }

    public class ListItemBetweenListsDTO
    {
        public ListItemDTO previous { get; set; }
        public ListItemDTO current { get; set; }
    }
    
}