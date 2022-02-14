using System;

namespace API.Entity {
    public class Task {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool isCompleted { get; set; }
        public Card Card { get; set; }
        public Guid CardId { get; set; }
    }
}