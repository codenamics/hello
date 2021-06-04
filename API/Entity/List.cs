using System.Collections.Generic;

namespace API.Entity
{
    public class List
    {
        public int Id { get; set; }
        public string Title { get; set; }
         public int Order { get; set; }
         public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}