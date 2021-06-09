using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entity
{
    public class Item
    {   [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
         public int Order { get; set; }
         public List List { get; set; }
        public Guid ListId { get; set; }
    }
}