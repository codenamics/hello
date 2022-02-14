using System;
using System.Collections.Generic;
using API.Entity;

namespace API.DTO {
    public class ListCardDTO {
        public Guid id { get; set; }
        public List<Card> cards { get; set; }
    }
}