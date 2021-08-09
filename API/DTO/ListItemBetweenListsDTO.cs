using System;
using System.Collections.Generic;
using API.DTO;

namespace API.Entity {
    public class ListItemBetweenListsDTO {
        public ListItemDTO container { get; set; }
        public ListItemDTO previousContainer { get; set; }
    }

}