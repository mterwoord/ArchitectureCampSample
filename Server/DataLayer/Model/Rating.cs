using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Model
{
    [Table("Ratings")]
    public class Rating : EntityBase
    {
        [NotMapped] // use navigation property
        public int SpeakerId { get; set; }

        public int SessionId { get; set; }

        [Required]
        public Speaker Speaker { get; set; }

        [Required]
        public SessionBase Session { get; set; }

        [Range(1, 6)]
        public int Rate { get; set; }

        [StringLength(500)]
        [MinLength(10)]
        public string Comment { get; set; }
    }
}
