using System;
using System.Collections.Generic;
using API.DTO;

namespace API.Entity {
    public class ListCardBetweenListsDTO {
        public ListCardDTO container { get; set; }
        public ListCardDTO previousContainer { get; set; }
    }

}