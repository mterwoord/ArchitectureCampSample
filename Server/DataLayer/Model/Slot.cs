using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Model
{
    [Table("Slots")]
    public class Slot : EntityBase
    {
        [Required]
        public DateTime Start { get; set; }

        public bool IsBreak { get; set; }

        public int SessionId { get; set; }
    }
}
