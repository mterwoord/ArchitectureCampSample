using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EndToEnd.DataLayer.Models
{
    [Table("Tracks")]
    public class Track : EntityBase
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public IList<SessionBase> Sessions { get; set; }
    }
}
