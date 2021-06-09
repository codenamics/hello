using System;
using System.Collections.Generic;

namespace API.Entity
{
    
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
 


    public class ListItemDTO
    {
        public Guid id { get; set; }
        public List<Item> items { get; set; }
    }

    public class Root
    {
        public ListItemDTO previous { get; set; }
        public ListItemDTO current { get; set; }
    }
    
}