using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EndToEnd.DataLayer.Models
{
    [ComplexType]
    public class Schedule
    {
        [StringLength(5)]
        [RegularExpression(@"[A-C]\.\d{2,3}")]
        public string Room { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? StartTime { get; set; }

        [NotMapped]
        public int SessionId { get; set; }
    }
}
