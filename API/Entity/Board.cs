using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entity {
    public class Board {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public ICollection<List> Lists { get; set; } = new List<List> ();
    }
}