using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Helpers;

namespace API.Entity
{
    public class List : IOrder
    {   [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public string Title { get; set; }
         public int Order { get; set; }
           public Board Board { get; set; }
        public Guid BoardId { get; set; }
         public ICollection<Card> Cards { get; set; } = new List<Card>();
    }
}