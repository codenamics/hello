using System.Collections.Generic;

namespace API.Entity
{
    public class Board
    {
        public int Id { get; set; }
        public string Name { get; set; }
         public ICollection<List> Lists { get; set; } = new List<List>();
    }
}