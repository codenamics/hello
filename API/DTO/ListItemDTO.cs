using System;
using System.Collections.Generic;
using API.Entity;

namespace API.DTO {
    public class ListItemDTO {
        public Guid id { get; set; }
        public List<Item> items { get; set; }
    }
}