using System;

namespace API.DTO {
    public class CardDTO {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}